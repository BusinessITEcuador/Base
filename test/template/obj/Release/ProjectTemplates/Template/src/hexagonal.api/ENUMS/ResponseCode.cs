namespace $safeprojectname$.enums
{
  public enum ResponseCode : int
  {
    OK = 200,
    OK_EXCEPTION = 3,
    NO_DATA = 1,
    GENERIC_ERROR = 500,
    UNIQUE_SESSION = 900,
    NOT_SECTION = 4,
    BAD_REQUEST = 400,
    BAD_REQUEST_CADUCADO = 401,
    BAD_REQUEST_EXIST = 402,
    NOT_FOUND = 404,
    NOT_FOUND_EXCEPTION = 5,
    REDIRECT = 301,
    ACCOUNT_ERROR = 6
  }
}
