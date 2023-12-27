using hexagonal.application.models.log;
using hexagonal.application.models.tramites;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace hexagonal.application.external
{
    public class TramiteClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TramiteClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _configuration = configuration;
        }

        public void GenerarAcuerdos(Datos datos, string lang)
        {
            try
            {
                string apiUrl = $"{_configuration.GetSection("Externos:Tramite:BaseUrl").Value}/api/Mensaje/GenerarPdfAcuerdos";

                string jsonRequest = JsonConvert.SerializeObject(new AcuerdosRequestModel { datos = datos });
                StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Agregar encabezado "Language"
                _httpClient.DefaultRequestHeaders.Add("Language", lang);

                HttpResponseMessage response = _httpClient.PostAsync(apiUrl, content).Result;
                string responseContent = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ObtenerMensaje(string codigo, string lang)
        {
            if(codigo== null || codigo == "" || codigo=="Default")
            {
                if (lang == "en")
                {
                    return "Sorry, there is a problem in our system.";
                }
                else
                {
                    return "Lo sentimos, hay un error en la plataforma";

                }
            }
            try
            {
                string apiUrl = $"{_configuration.GetSection("Externos:Tramite:BaseUrl").Value}/api/Idioma/MensajePorCodigo";
                string jsonRequest = JsonConvert.SerializeObject(new MensajeRequestModel { codigo = codigo, codigoIdioma=lang });
                StringContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                // Agregar encabezado "Language"
                _httpClient.DefaultRequestHeaders.Add("Language", lang);

                HttpResponseMessage response = _httpClient.PostAsync(apiUrl, content).Result;
                string responseContent = response.Content.ReadAsStringAsync().Result;

                MensajeResponseModel respuesta = JsonConvert.DeserializeObject<MensajeResponseModel>(responseContent);


                if (response.IsSuccessStatusCode)
                {
                    return respuesta.data;
                }
                else
                {
                    throw new Exception(respuesta.message);
                }
            }
            catch (SqlException ex)
            {
                if (lang == "en")
                {
                    return "Sorry, there is a problem in our system.";
                }
                else
                {
                    return "Lo sentimos, hay un error en la plataforma";

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}