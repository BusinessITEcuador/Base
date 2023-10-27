using $safeprojectname$.Controllers.bases;
using hexagonal.application.models.person;
using hexagonal.application.services.person.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PersonController : APIControllerBase
  {
    private new readonly ILogger<PersonController> _logger;
    private readonly IPersonService _PersonService;
    public PersonController(ILogger<PersonController> logger,
                  IPersonService PersonService) : base(logger)
    {
      this._logger = logger;
      this._PersonService = PersonService;
    }

    [HttpGet]
    public IActionResult GetChecks()
    {
      try
      {
        var acounts = _PersonService.GetAll();
        return Success(acounts);
      }
      catch (Exception exc)
      {
        this._logger.LogError(exc, exc.Message);
        return this.BadRequest(exc.Message);
      }
    }

    [HttpPost]
    public IActionResult Add(PersonRequestModel Person)
    {
      try
      {
        _PersonService.Add(Person);
        return Success();
      }
      catch (Exception exc)
      {
        this._logger.LogError(exc, exc.Message);
        return this.BadRequest(exc.Message);
      }
    }
  }
}
