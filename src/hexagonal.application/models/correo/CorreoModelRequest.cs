namespace hexagonal.application.models.correo
{
    public class CorreoModelRequest
    {
        public byte[] PdfBytes { get; set; }

        public string NombreArchivo { get; set; }
    }
}