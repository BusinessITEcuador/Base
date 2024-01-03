using Azure.Identity;
using hexagonal.api.Controllers.bases;
using hexagonal.application.external;
using hexagonal.application.models.log;
using hexagonal.application.models.tramites;
using hexagonal.application.models.usuario;
using hexagonal.application.services.log.interfaces;
using hexagonal.application.services.registroPersona.interfaces;
using hexagonal.application.services.usuario.interfaces;
using hexagonal.infrastructure.api.Controllers.bases;
using hexagonal.infrastructure.api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace hexagonal.infrastructure.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : APIControllerBase
    {
        private new readonly ILogger<UsuarioController> _logger;

        private readonly GraphApiOptions _graphApiOptions;

        private readonly IUsuarioService _UsuarioService;

        private readonly ICuentaService _cuentaService;

        private readonly IRegistroPersonaService _registroPersonaService;

        private readonly TramiteClient _tramiteClient;

        private readonly CorreoClient _correoClient;

        private readonly ILogService _logService;
        public UsuarioController(IOptions<GraphApiOptions> graphApiOptions,
            IUsuarioService UsuarioService,
            IRegistroPersonaService registroPersonaService,
            ICuentaService cuentaService,
            ILogService logService,
            TramiteClient tramiteClient,
            CorreoClient correoClient,
            ILogger<UsuarioController> logger) : base(logger)
        {
            this._logger = logger;
            this._graphApiOptions = graphApiOptions.Value;
            this._UsuarioService = UsuarioService;
            this._registroPersonaService = registroPersonaService;
            this._cuentaService = cuentaService;
            this._tramiteClient = tramiteClient;
            this._logService = logService;
            this._correoClient = correoClient;
        }

        //[Authorize(AuthenticationSchemes = "JwtBearerAuth,Bearer")]
        [HttpPost]
        [Route("ObtenerConLog")]
        public IActionResult GetById([FromBody] UsuarioLogRequestModel usuario)
        {
            var lang = Request.Headers["Language"].FirstOrDefault();
            if (lang == null)
            lang = "es";
            try
            {
                var correo = string.Empty;
                var scopes = new[] { _graphApiOptions.Scopes };
                var tenantId = _graphApiOptions.TenantId;
                var clientId = _graphApiOptions.ClientId;
                var clientSecret = _graphApiOptions.ClientSecret;
                var clientSecretCredential = new ClientSecretCredential(
                    tenantId, clientId, clientSecret);
                var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

                var Cuenta = _cuentaService.GetById(usuario.Id);
                if (Cuenta == null)
                {
                    //obtener correo de b2c
                    try
                    {
                        var usuarioCuenta = graphClient.Users[usuario.Id.ToString()].GetAsync().Result.GivenName;
                        if (usuarioCuenta != null)
                        {
                            correo = usuarioCuenta;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("An error occurred:"+exc.Message);
                        throw new Exception(_tramiteClient.ObtenerMensaje("B2CNoExisteCuenta",lang));
                    }
                }
                else
                {
                    correo = Cuenta.Correo;
                }
                //guardar log
                if (usuario.Log)
                {
                    try
                    {
                        var log = new LogRequestModel();
                        log.Estado = "EXITOSO";
                        log.Usuario = correo;
                        log.Altitud = usuario.Altitud;
                        log.Latitud = usuario.Latitud;
                        log.Longitud = usuario.Longitud;
                        log.IpPublica = usuario.IpPublica;
                        _logService.Add(log);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("An error occurred:"+exc.Message);
                        throw new Exception(_tramiteClient.ObtenerMensaje("LogErrorInicio", lang));
                    }
                }
                var Usuario = _UsuarioService.GetByCorreo(correo);
                if (Usuario == null)
                {
                    return this.Success(new { exist = false });
                }
                else
                {
                    if (Usuario.SegundoApellido.IsNullOrEmpty())
                    {
                        Usuario.SegundoApellido = string.Empty;
                    }
                    if (Usuario.SegundoApellido.Length > 0)
                    {
                        Usuario.SegundoApellido = " " + Usuario.SegundoApellido;
                    }
                    var nombreCompleto = Usuario.Nombres + " " + Usuario.PrimerApellido + Usuario.SegundoApellido;

                    //validar nuevamente si la cuenta no existe para colocarla como validado
                    var CuentaValidar = new CuentaResponseModel();
                    try
                    {
                        CuentaValidar = _cuentaService.GetById(usuario.Id);
                    }
                    catch
                    {
                        throw new Exception(_tramiteClient.ObtenerMensaje("BDDCuentaNoExiste", lang));
                    }
                    if (CuentaValidar == null)
                    {
                        try
                        {
                            _cuentaService.AgregarCuenta(usuario.Id, Usuario.Correo, Usuario.Id);
                            var patchData = new Dictionary<string, object>
                            {
                                { $"extension_{_graphApiOptions.ExtensionId}_validado", "true" }
                            };
                            //validacion de segundo apellido
                            var userUpdate = new Microsoft.Graph.Models.User
                            {
                                AdditionalData = patchData,
                                DisplayName = nombreCompleto
                            };

                            var result = graphClient.Users[usuario.Id.ToString()].PatchAsync(userUpdate).Result;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("An error occurred:"+exc.Message);
                            throw new Exception(_tramiteClient.ObtenerMensaje("B2CErrorConnection", lang));
                        }
                    }

                    
                    
                    return Success(new { exist = true, nombres = Usuario.Nombres, primerApellido = Usuario.PrimerApellido, segundoApellido = Usuario.SegundoApellido, nombreCompleto = nombreCompleto, fechaNacimiento = Usuario.FechaNacimiento, nacionalidad = Usuario.IdNacionalidad });
                }
                
            }
            catch (Exception exc)
            {
                _logger.LogError("An error occurred:"+exc.Message);
                return this.BadRequest(exc.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "JwtBearerAuth,Bearer")]
        [HttpPost]
        [Route("GuardarDatosMinimos")]
        public IActionResult GuardarDatosMinimos([FromBody] UsuarioRequestModel usuario)
        {
            var httpContext = HttpContext;
            Guid userId = Guid.Empty;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "aud");
                
                if (userIdClaim != null)
                {
                    Guid.TryParse(userIdClaim.Value, out userId);
                }

                if (userId == Guid.Empty)
                {
                    var userIdClaim2 = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                    if (userIdClaim2 != null)
                    {
                        Guid.TryParse(userIdClaim2.Value, out userId);
                    }
                }
            }
            bool eliminarUsuario = false;
            var lang = Request.Headers["Language"].FirstOrDefault();
            if (lang == null)
            lang = "es";
            try
            {
                var scopes = new[] { _graphApiOptions.Scopes };
                var tenantId = _graphApiOptions.TenantId;
                var clientId = _graphApiOptions.ClientId;
                var clientSecret = _graphApiOptions.ClientSecret;
                var clientSecretCredential = new ClientSecretCredential(
                    tenantId, clientId, clientSecret);
                var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

                //validar campos opcionales
                if (usuario.SegundoApellido.IsNullOrEmpty())
                {
                    usuario.SegundoApellido = string.Empty;
                }

                if (usuario.CorreoAlternativo.IsNullOrEmpty())
                {
                    usuario.CorreoAlternativo = string.Empty;
                }

                if (usuario.IpPublica.IsNullOrEmpty())
                {
                    usuario.IpPublica = string.Empty;
                }

                if (usuario.Altitud.IsNullOrEmpty())
                {
                    usuario.Altitud = string.Empty;
                }

                if (usuario.Latitud.IsNullOrEmpty())
                {
                    usuario.Latitud = string.Empty;
                }

                if (usuario.Longitud.IsNullOrEmpty())
                {
                    usuario.Longitud = string.Empty;
                }

                //obtener configuracion de registro persona
                try
                {
                    var configuracion = _registroPersonaService.ObtenerConfiguracionDePersona().Result;

                    if (configuracion == null)
                    {
                        return BadRequest(_tramiteClient.ObtenerMensaje("RegistroPersonaErrorNullConfiguration", lang));
                    }
                    var edad = _registroPersonaService.CalcularEdad(usuario.FechaNacimiento);
                    if (edad < configuracion.EdadMinima)
                    {
                        eliminarUsuario = true;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred:"+exc.Message);
                    return BadRequest(_tramiteClient.ObtenerMensaje("RegistroPersonaErrorConfiguration", lang));
                }

                //Buscar si el correo alternativo ya se encuentra en usuario
                try
                {
                    var usuarioCorreoExistente = _UsuarioService.GetByCorreoAlternativo(usuario.CorreoAlternativo);
                    if (usuarioCorreoExistente != null)
                    {
                        return this.Success(new { mensaje = _tramiteClient.ObtenerMensaje("CorreoAlternativoExistente", lang)});
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred:"+exc.Message);
                    return BadRequest(_tramiteClient.ObtenerMensaje("VerificacionCorreoAlternativoError", lang));
                }

                //Obtener el usuario de B2C
                try
                {
                    var userGraph = graphClient.Users[usuario.Id.ToString()].GetAsync().Result;
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred:"+exc.Message);
                    return BadRequest(_tramiteClient.ObtenerMensaje("B2CNoExisteCuenta", lang));
                }
                if (usuario.SegundoApellido.Length > 0)
                {
                    usuario.SegundoApellido = " " + usuario.SegundoApellido;
                }
                var nombreCompleto = usuario.Nombres + " " + usuario.PrimerApellido + usuario.SegundoApellido;

                //eliminar usuario si es el caso
                if (eliminarUsuario)
                {
                    try
                    {
                        //crearjson con nombres
                        var json = new Dictionary<string, string>
                            {
                                { "namePersona", nombreCompleto }
                            };

                            try
                            {
                                var eliminado = graphClient.Users[usuario.Id.ToString()].DeleteAsync();

                            }
                            catch
                            {
                            throw new Exception(_tramiteClient.ObtenerMensaje("B2CDeleteUser", lang));
                        }
                            
                            var jsonSerialize = JsonConvert.SerializeObject(json);
                            try
                            {
                                _correoClient.EnviarCorreo(null, usuario.Correo, "Acceso denegado a menor de edad", "PERSONA.CREACION.MENOREDAD", jsonSerialize, lang);
                            }
                            catch (Exception ex)
                            {
                            _logger.LogError("An error occurred:"+exc.Message);
                            throw new Exception(_tramiteClient.ObtenerMensaje("SMTPError", lang));
                        }

                        return Success(_tramiteClient.ObtenerMensaje("B2CDeleteUserSuccess", lang));

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

               
                //Actualizar el usuario de B2C
                try
                {
                    var patchData = new Dictionary<string, object>
                    {
                        { $"extension_{_graphApiOptions.ExtensionId}_validado", "true" }
                    };
                    //validacion de segundo apellido
                    var userUpdate = new Microsoft.Graph.Models.User
                    {
                        AdditionalData = patchData,
                        DisplayName = nombreCompleto
                    };

                    var result = graphClient.Users[usuario.Id.ToString()].PatchAsync(userUpdate).Result;
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred:"+exc.Message);
                    return BadRequest(_tramiteClient.ObtenerMensaje("B2CUpdateInformation", lang));

                }

                //Agregar el usuario en la base de batos
                try
                {
                    var usuarioExistente = _UsuarioService.GetByCorreo(usuario.Correo);
                    
                    //verificar que no exista usuario
                    if (usuarioExistente == null)
                    {
                        try
                        {
                             _UsuarioService.Add(usuario, userId);
                            var usuarioNuevo = _UsuarioService.GetByCorreo(usuario.Correo);
                            //enviar acuerdos
                            try
                            {
                                Datos datos = new Datos();
                                datos.nombres = usuario.Nombres;
                                datos.primerApellido = usuario.PrimerApellido;
                                datos.segundoApellido = usuario.SegundoApellido;
                                datos.fechaNacimiento = usuario.FechaNacimiento;
                                datos.correo = usuario.Correo;
                                datos.correoAlternativo = usuario.CorreoAlternativo;
                                datos.nacionalidad = usuario.Nacionalidad;
                                datos.tipoIdentificacion = usuario.TipoIdentificacion;
                                datos.sexo = usuario.Sexo;
                                datos.genero = usuario.Genero;
                                datos.numeroIdentificacion = usuario.NumeroIdentificacion;
                                datos.id = usuarioNuevo.Id;
                                _tramiteClient.GenerarAcuerdos(datos, lang);
                            }
                            catch
                            {
                                return BadRequest(_tramiteClient.ObtenerMensaje("TCAcuerdosError", lang));
                            }
                        }
                        catch
                        {
                            return BadRequest(_tramiteClient.ObtenerMensaje("BDDUsuarioErrorCreate", lang));
                        }
                        
                        

                    }
                    else
                    {
                        var cuentaExistente = _cuentaService.GetById(usuario.Id);
                        if (cuentaExistente == null)
                        {
                            _cuentaService.AgregarCuenta(usuario.Id, usuario.Correo,usuarioExistente.Id);
                        }
                    }
                }
                catch (Exception exc)
                {
                    try
                    {
                        var patchData = new Dictionary<string, object>
                    {
                        { $"extension_{_graphApiOptions.ExtensionId}_validado", "false" }
                    };
                        //validacion de segundo apellido
                        var userUpdate = new Microsoft.Graph.Models.User
                        {
                            AdditionalData = patchData,
                            DisplayName = nombreCompleto
                        };

                        var result = graphClient.Users[usuario.Id.ToString()].PatchAsync(userUpdate).Result;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("An error occurred:"+exc.Message);
                        return BadRequest(_tramiteClient.ObtenerMensaje("B2CUpdateInformation", lang));
                    }
                    return BadRequest(_tramiteClient.ObtenerMensaje("BDDCuentaErrorCreate", lang));
                }

                return Success();
            }
            catch (Exception exc)
            {
                _logger.LogError("An error occurred:"+exc.Message);
                return this.BadRequest(_tramiteClient.ObtenerMensaje(string.Empty, lang));
            }
        }

        [HttpGet]
        [Route("ObtenerUsuarioPorCuentaId/{cuentaId}")]
        public IActionResult ObtenerUsuarioIdPorCuentaId(Guid cuentaId)
        {
            var lang = Request.Headers["Language"].FirstOrDefault();
            if (lang == null)
                lang = "es";
            try
            {
                var usuario = _UsuarioService.ObtenerUsuarioPorCuentaId(cuentaId);
                return Success(usuario);
            }
            catch (Exception exc)
            {
                _logger.LogError("An error occurred:"+exc.Message);
                return this.BadRequest(_tramiteClient.ObtenerMensaje("BDDUsuarioPorCuenta",lang));
            }
        }
    }
}