using hexagonal.api.Controllers.bases;
using hexagonal.application.models.genero;
using hexagonal.application.services.genero.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hexagonal.infrastructure.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : APIControllerBase
    {
        private new readonly ILogger<GeneroController> _logger;
        private readonly IGeneroService _GeneroService;

        public GeneroController(ILogger<GeneroController> logger,
            IGeneroService GeneroService) : base(logger)
        {
            this._logger = logger;
            this._GeneroService = GeneroService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var genero = _GeneroService.GetById(id);
                return Success(genero);
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
                var generos = _GeneroService.GetAll();
                return Success(generos);
            }
            catch (Exception exc)
            {
                this._logger.LogError(exc, "Error getting genres");
                return this.BadRequest(exc.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] GeneroRequestModel Genero)
        {
            try
            {
                _GeneroService.Add(Genero);
                return Success();
            }
            catch (Exception exc)
            {
                this._logger.LogError(exc, "Error setting genre");
                return this.BadRequest(exc.Message);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] GeneroRequestModel Genero, Guid id)
        {
            try
            {
                _GeneroService.Update(Genero, id);
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
                _GeneroService.Delete(id);
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