using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.models
{
  public class PagedCollection<T>
  {
    public int Offset { get; set; }

    public int Limit { get; set; }

    public int Size { get; set; }

    public T[] Items { get; set; }

  }
}
