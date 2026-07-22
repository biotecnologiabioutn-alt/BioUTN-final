namespace BioUTN.MVC.Models
{
    public class EspecieViewModel
    {
        public string CodigoInterno { get; set; } = string.Empty;
        public string NombreCientifico { get; set; } = string.Empty;
        public string Familia { get; set; } = string.Empty;
        public string NombreComun { get; set; } = string.Empty;
        public string TipoPlanta { get; set; } = string.Empty;
        public string CicloVida { get; set; } = string.Empty;
        public string CategoriaBiotecnologica { get; set; } = string.Empty;
        public string EstadoIucn { get; set; } = string.Empty;
        public string OrigenBiogeografico { get; set; } = string.Empty;
        public int LotesVinculados { get; set; } = 0; // Para regla de validación de borrado
    }
}
