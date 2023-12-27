using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.domain.entities
{
    public class LogEntity
    {
        public Guid Id { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; }
        public string Altitud { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string IpPublica { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }
    }
}
