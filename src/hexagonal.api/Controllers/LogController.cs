using hexagonal.api.Controllers.bases;
using hexagonal.application.models.log;
using hexagonal.application.services.log.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hexagonal.infrastructure.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : APIControllerBase
    {
        private new readonly ILogger<LogController> _logger;
        private readonly ILogService _LogService;

        public LogController(ILogger<LogController> logger,
            ILogService LogService) : base(logger)
        {
            this._logger = logger;
            this._LogService = LogService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] LogRequestModel Log)
        {
            try
            {
                _LogService.Add(Log);
                return Success();
            }
            catch (Exception exc)
            {
                this._logger.LogError(exc, "Error setting genre");
                return this.BadRequest(exc.Message);
            }
        }
    }
}