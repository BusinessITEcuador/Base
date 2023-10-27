using $safeprojectname$.enums;
using $safeprojectname$.models;
using Microsoft.AspNetCore.Mvc;

namespace $safeprojectname$.Controllers.bases
{
  [ApiController]
  public abstract class APIControllerBase : ControllerBase
  {
    protected readonly ILogger _logger;

    protected APIControllerBase(ILogger logger)
    {
      this._logger = logger;
    }

    protected ActionResult Success(object? data = null, ResponseCodeText codeText = ResponseCodeText.SUCCESS)
    {
      return Ok(new ResponseBase()
      {
        Code = ResponseCode.OK,
        Data = data,
        CodeText = codeText.ToString()
      });
    }
    protected ActionResult SuccessUniqueSession(object? data = null, ResponseCodeText codeText = ResponseCodeText.SUCCESS)
    {
      return Ok(new ResponseBase()
      {
        Code = ResponseCode.UNIQUE_SESSION,
        Data = "Ya existe una sesion activa en otro dispositivo desea transferirla a este nuevo dispositivo",
        CodeText = data.ToString()
      });
    }
    protected ActionResult SuccessWithCode(string codeText, object? data = null)
    {
      return Ok(new ResponseBase()
      {
        Code = ResponseCode.OK_EXCEPTION,
        CodeText = codeText,
        Data = data
      });
    }

    protected ActionResult Ok(string message, object data)
    {
      return Ok(new ResponseBase()
      {
        Code = ResponseCode.OK,
        Message = "Exitoso",
        Data = data
      });
    }

    protected ActionResult NoData()
    {
      return Ok(new ResponseBase()
      {
        Code = ResponseCode.NO_DATA,
        Message = "No existe data"
      });
    }

    protected new ActionResult NotFound()
    {
      return NotFound(new ResponseBase()
      {
        Code = ResponseCode.NOT_FOUND,
        Message = "Registro no encontrado"
      });
    }
    protected ActionResult NotFoundWithCode(string codeText)
    {
      return Ok(new ResponseBase()
      {
        Code = ResponseCode.NOT_FOUND_EXCEPTION,
        CodeText = codeText
      });
    }

    protected ActionResult BadRequest()
    {
      return BadRequestWithData(null);
    }
    protected ActionResult BadRequest(string message)
    {
      return BadRequest(new ResponseBase()
      {
        Code = ResponseCode.BAD_REQUEST,
        Message = message
      });
    }
    protected ActionResult BadRequestCaducado(string message)
    {
      return BadRequest(new ResponseBase()
      {
        Code = ResponseCode.BAD_REQUEST_CADUCADO,
        Message = message
      });
    }
    protected ActionResult BadRequestUserExist(string message)
    {
      return BadRequest(new ResponseBase()
      {
        Code = ResponseCode.BAD_REQUEST_EXIST,
        Message = message
      });
    }
    protected ActionResult BadRequestWithData(object data)
    {
      return BadRequest(new ResponseBase()
      {
        Code = ResponseCode.BAD_REQUEST,
        Message = "Lo siento no se pudo procesar la solicitud. Intentenlo mas tarde",
        Data = data
      });
    }

    protected ActionResult BadRequestWithCode(string codeText, string message)
    {
      return BadRequest(new ResponseBase()
      {
        Code = ResponseCode.BAD_REQUEST,
        CodeText = codeText,
        Message = message
      });
    }
    protected ActionResult Error()
    {
      return Error("Error generico");
    }

    protected ActionResult Error(string message)
    {
      return BadRequest(new ResponseBase()
      {
        Code = ResponseCode.GENERIC_ERROR,
        Message = message
      });
    }
  }
}
