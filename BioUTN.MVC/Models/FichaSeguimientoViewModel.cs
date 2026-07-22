using System;
using System.Collections.Generic;
using BioUTN.Modelos;

namespace BioUTN.MVC.Models
{
    public class FichaSeguimientoViewModel
    {
        public int IdLoteCultivo { get; set; }
        public string LoteCodigo { get; set; } = string.Empty;
        public DateTime FechaSiembra { get; set; }
        public int TotalUnidades { get; set; }
        public string Explante { get; set; } = string.Empty;
        public string Responsables { get; set; } = string.Empty;
        
        public DateTime FechaRevision { get; set; } = DateTime.Today;
        public int TiempoEnDias => (FechaRevision.Date - FechaSiembra.Date).Days;

        public List<FichaFilaViewModel> Filas { get; set; } = new List<FichaFilaViewModel>();
    }

    public class FichaFilaViewModel
    {
        public int IdUnidadFrasco { get; set; }
        public string CodigoUnidad { get; set; } = string.Empty;
        public bool Bacterias { get; set; }
        public bool Hongos { get; set; }
        public bool Oxidacion { get; set; }
        public bool Muerte { get; set; }
        public string Respuesta { get; set; } = string.Empty;
        public string Observacion { get; set; } = string.Empty;
    }
}
