using hexagonal.api.Controllers.bases;
using hexagonal.application.models.idioma;
using hexagonal.application.services.idioma.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hexagonal.infrastructure.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdiomaController : APIControllerBase
    {
        private new readonly ILogger<IdiomaController> _logger;
        private readonly IIdiomaService _IdiomaService;

        public IdiomaController(ILogger<IdiomaController> logger,
            IIdiomaService IdiomaService) : base(logger)
        {
            this._logger = logger;
            this._IdiomaService = IdiomaService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            
            try
            {
                var Idioma = _IdiomaService.GetById(id);
                return Success(Idioma);
            }
            catch (Exception exc)
            {
                this._logger.LogError(exc, "Error getting genre by id: ", id);
                return this.BadRequest(exc.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var Idiomas = _IdiomaService.GetAll();
                return Success(Idiomas);
            }
            catch (Exception exc)
            {
                this._logger.LogError(exc, "Error getting genres");
                return this.BadRequest(exc.Message);
            }
        }

        [HttpPost]
        [Authorize (AuthenticationSchemes = "JwtBearerAuth")]
        public IActionResult Add([FromBody] IdiomaRequestModel Idioma)
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
            try
            {
                _IdiomaService.Add(Idioma,userId);
                return Success();
            }
            catch (Exception exc)
            {
                this._logger.LogError(exc, "Error setting genre");
                return this.BadRequest(exc.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] IdiomaRequestModel Idioma, Guid id)
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
            try
            {
                _IdiomaService.Update(Idioma, id,userId);
                return Success();
            }
            catch (Exception exc)
            {
                this._logger.LogError(exc, "Error updating genre id: ", id);
                return this.BadRequest(exc.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] Guid id)
        {
            try
            {
                _IdiomaService.Delete(id);
                return Success();
            }
            catch (Exception exc)
            {
                this._logger.LogError(exc, "Error deleting genre id: ", id);
                return this.BadRequest(exc.Message);
            }
        }
    }
}