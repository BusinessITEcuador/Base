
using hexagonal.application.models.correo;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Text;

namespace hexagonal.application.external
{
    public class CorreoClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly TramiteClient _tramiteClient;

        public CorreoClient(HttpClient httpClient, IConfiguration configuration, TramiteClient tramiteClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration;
            _tramiteClient = tramiteClient;
        }

        public void EnviarCorreo(List<CorreoModelRequest> archivos, string destinatarios, string asunto, string codigo, string masDatos, string lang)
        {
            bool masDatosValido = !string.IsNullOrEmpty(masDatos);
            try
            {
                var ruta3 = $"{_configuration.GetSection("Externos:PowerAutomate:FlujoCorreo").Value}";
                var formData = new MultipartFormDataContent();


                string json = $@"{{
                    ""boolFiles"": {(archivos != null ? "true" : "false")},
                    ""codigoPlantilla"": ""{codigo}"",
                    ""listaDestinatarios"": ""{destinatarios}"",
                    ""txtAsunto"": ""{asunto}"",
                    ""masDatos"": {(masDatosValido ? masDatos : "{}")}
                }}";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                formData.Add(content, "formdata");
                if(archivos!=null)
                {
                    foreach (var archivo in archivos)
                    {
                        var archivoContent = new ByteArrayContent(archivo.PdfBytes);
                        archivoContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                        formData.Add(archivoContent, "filedata", $"{archivo.NombreArchivo}.pdf");
                    }
                }
                
                HttpResponseMessage response3 = _httpClient.PostAsync(ruta3, formData).Result;
                if (!response3.IsSuccessStatusCode)
                {
                    throw new Exception(_tramiteClient.ObtenerMensaje("PowerAutomateCorreoError", lang));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(_tramiteClient.ObtenerMensaje("PowerAutomateCorreoError", lang));
            }

        }
    }
}
