namespace BioUTN.MVC.Models
{
    public class ProyectoViewModel
    {
        public string IdLote { get; set; } = string.Empty;
        public string NombreCientifico { get; set; } = string.Empty;
        public string TipoProyecto { get; set; } = string.Empty; // "Tesis" o "Docencia"
        public string Etiqueta { get; set; } = string.Empty;
        public string Detalles { get; set; } = string.Empty;
    }
}
