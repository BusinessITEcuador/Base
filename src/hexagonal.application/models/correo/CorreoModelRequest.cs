using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.application.models.correo
{
    public class CorreoModelRequest
    {
        public byte[] PdfBytes { get; set; }

        public string NombreArchivo { get; set; }
    }
}
