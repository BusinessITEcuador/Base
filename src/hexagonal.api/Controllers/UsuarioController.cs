using Azure.Identity;
using hexagonal.api.Controllers.bases;
using hexagonal.application.models.usuario;
using hexagonal.application.services.registroPersona.interfaces;
using hexagonal.application.services.usuario.interfaces;
using hexagonal.application.services.usuario;
using hexagonal.infrastructure.api.Controllers.bases;
using hexagonal.infrastructure.api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.IdentityModel.Tokens;

namespace hexagonal.infrastructure.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : APIControllerBase
    {
        private new readonly ILogger<UsuarioController> _logger;

        private readonly GraphApiOptions _graphApiOptions;


        private readonly IUsuarioService _UsuarioService;

        private readonly IRegistroPersonaService _registroPersonaService;
        public UsuarioController(IOptions<GraphApiOptions> graphApiOptions,
            IUsuarioService UsuarioService,
            IRegistroPersonaService registroPersonaService,
            ILogger<UsuarioController> logger) : base(logger)
        {
            this._logger = logger;
            this._graphApiOptions = graphApiOptions.Value;
            this._UsuarioService = UsuarioService;
            this._registroPersonaService = registroPersonaService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        [ValidateId]
        public IActionResult GetById([FromRoute] string id)
        {
            var idioma = Request.Headers["Language"].FirstOrDefault();
            if(idioma == null)
            {
                idioma = "es";
            }
            try
            {
                var Usuario = _UsuarioService.GetById(Guid.Parse(id));
                if (Usuario == null)
                {
                    return this.Success(new { exist = false });
                }
                if (Usuario.SegundoApellido.IsNullOrEmpty())
                {
                    Usuario.SegundoApellido = string.Empty;
                }
                if (Usuario.SegundoApellido.Length > 0)
                {
                    Usuario.SegundoApellido = " " + Usuario.SegundoApellido;
                }
                var nombreCompleto = Usuario.Nombres + " " + Usuario.PrimerApellido + Usuario.SegundoApellido;

                return Success(new { exist = true , nombreCompleto = nombreCompleto });
            }
            catch (Exception exc)
            {
                return this.BadRequest($"Problema de conexión con la base de datos");
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [Route("GuardarDatosMinimos")]
        public IActionResult GuardarDatosMinimos([FromBody] UsuarioRequestModel usuario)
        {
            var httpContext = HttpContext;
            Guid userId = Guid.Empty;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "aud");

                Guid.TryParse(userIdClaim.Value, out userId);

                if (userId == Guid.Empty)
                {
                    var userIdClaim2 = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                    Guid.TryParse(userIdClaim2.Value, out userId);
                }
            }
            bool eliminarUsuario = false;
            var idioma = Request.Headers["Language"].FirstOrDefault();
            if (idioma == null)
            {
                idioma = "es";
            }
            try
            {
                var scopes = new[] { _graphApiOptions.Scopes};
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
                    usuario.CorreoAlternativo = "";
                }

                

                //obtener configuracion de registro persona
                try
                {
                    var configuracion = _registroPersonaService.ObtenerConfiguracionDePersona().Result;

                    if (configuracion == null)
                    {
                        if (idioma == "es")
                        {
                            return BadRequest("No existe configuracion de registro persona");
                        }
                        else
                        {
                            return BadRequest("No registration person configuration found");
                        }
                       
                    }
                    var edad = _registroPersonaService.CalcularEdad(usuario.FechaNacimiento);
                    if (edad < configuracion.EdadMinima)
                    {
                        eliminarUsuario = true;
                    }
                }
                catch (Exception ex)
                {
                    //agregar error en español y en ingles 
                    if(idioma=="es")
                    {
                        return this.BadRequest("Error al obtener la configuracion de registro persona");
                    }
                    else
                    {
                        return this.BadRequest("Error retrieving registration person configuration.");
                    }
                }

                //Buscar si el correo alternativo ya se encuentra en usuario
                try
                {
                    var usuarioCorreoExistente = _UsuarioService.GetByCorreo(usuario.CorreoAlternativo);
                    if (usuarioCorreoExistente != null)
                    {
                        if (idioma == "es")
                        {
                            return BadRequest($"El correo alternativo ya se encuentra registrado en otra cuenta, ingrese un correo diferente");
                        }
                        else
                        {
                            return BadRequest($"The alternate email is already registered in another account, please enter a different email");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (idioma == "es")
                    {
                        return BadRequest($"Error al obtener verificación de correo alternativo");
                    }
                    else
                    {
                        return BadRequest($"Error retrieving alternate email verification");
                    }
                }


                //Obtener el usuario de B2C
                try
                {
                    var userGraph = graphClient.Users[usuario.Id.ToString()].GetAsync().Result;

                }
                catch(Exception ex)
                {
                    if (idioma == "es")
                    {
                        return BadRequest($"Usuario con id {usuario.Id} no existe");
                    }
                    else
                    {
                        return BadRequest($"User with id {usuario.Id} not exist");
                    }
                }
                //eliminar usuario si es el caso
                if (eliminarUsuario)
                {
                    try
                    {
                        var eliminado = graphClient.Users[usuario.Id.ToString()].DeleteAsync();
                        if (idioma == "es")
                        {
                            return Success("Usuario eliminado exitosamente");
                        }
                        else
                        {
                            return Success("User deleted successfully");
                        }
                    }
                    catch(Exception ex)
                    {
                        if (idioma == "es")
                        {
                            return BadRequest($"No se pudo eliminar el usuario");
                        }
                        else
                        {
                            return BadRequest($"User cannot be deleted");
                        }
                    }
                }
                //Agregar el usuario en la base de batos
                try
                {
                    //verificar que no exista
                    var usuarioExistente = _UsuarioService.GetById(usuario.Id);
                    if (usuarioExistente == null)
                    {
                        _UsuarioService.Add(usuario,userId);
                    }
                }
                catch (Exception exc)
                {
                    return this.BadRequest("Hubo un error almacenando sus datos en el sistema");
                }

                if (usuario.SegundoApellido.Length > 0)
                {
                    usuario.SegundoApellido = " " + usuario.SegundoApellido;
                }
                var nombreCompleto = usuario.Nombres + " " + usuario.PrimerApellido + usuario.SegundoApellido;

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
                catch
                {
                    if(idioma == "es")
                    {
                        return this.BadRequest("No se pudo actualizar la información de registro");
                    }
                    else
                    {
                        //poner el mensaje en ingles
                        return this.BadRequest("Cannot update the register information");
                    }
                }


                return Success();
            }
            catch (Exception exc)
            {
                return this.BadRequest("Error no controlado, inténtelo nuevamente");
            }
        }

       
    }
}