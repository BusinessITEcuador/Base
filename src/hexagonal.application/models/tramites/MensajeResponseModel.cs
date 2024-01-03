using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hexagonal.application.models.tramites
{
    public class MensajeResponseModel
    {
        public int? code { get; set; }
        public string? message { get; set; }
        public string? data { get; set; }
        public string? codeText { get; set; }

        public MensajeResponseModel()
        {
            code = 0;
            message = string.Empty;
            data = string.Empty;
            codeText = string.Empty;
        }
    }
}
