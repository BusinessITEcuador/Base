using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.exceptions
{
  public class NotFoundException : Exception
  {

    public NotFoundException()
    {
      throw new Exception("No existe un registro!");
    }
    public NotFoundException(string message) : base(message)
    {
    }
  }
}
