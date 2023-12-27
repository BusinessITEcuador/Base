using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.application.models.usuario
{
    public class UsuarioLogRequestModel
    {
        public Guid Id { get; set; }
        public string? Altitud { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? IpPublica { get; set; }
        public bool Log { get; set; }

        public UsuarioLogRequestModel()
        {
            Id = Guid.NewGuid();
            Altitud = string.Empty;
            Latitud = string.Empty;
            Longitud = string.Empty;
            IpPublica = string.Empty;
            Log = false;
        }
    }
}
