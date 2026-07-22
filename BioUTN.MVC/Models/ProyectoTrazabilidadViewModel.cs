using System;
using System.Collections.Generic;
using BioUTN.Modelos;

namespace BioUTN.MVC.Models
{
    public class TrazabilidadProyectoViewModel
    {
        public int ProyectoId { get; set; }
        public string NombreProyecto { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public List<TrazabilidadPlantaMadreNode> PlantasMadres { get; set; } = new List<TrazabilidadPlantaMadreNode>();
    }

    public class TrazabilidadPlantaMadreNode
    {
        public int Id { get; set; }
        public string CodigoAsignado { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public string EstadoFitosanitario { get; set; } = string.Empty;
        public List<TrazabilidadLoteNode> Lotes { get; set; } = new List<TrazabilidadLoteNode>();
    }

    public class TrazabilidadLoteNode
    {
        public int Id { get; set; }
        public string CodigoTrazabilidad { get; set; } = string.Empty;
        public string TipoExplante { get; set; } = string.Empty;
        public DateTime FechaSiembra { get; set; }
        public string Fase { get; set; } = string.Empty;
        public int Repique { get; set; } = 0;
        public List<TrazabilidadFrascoNode> Frascos { get; set; } = new List<TrazabilidadFrascoNode>();
        public List<TrazabilidadLoteNode> LotesHijos { get; set; } = new List<TrazabilidadLoteNode>(); // Resiembras
    }

    public class TrazabilidadFrascoNode
    {
        public int Id { get; set; }
        public string CodigoUnidad { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public string EstadoFitosanitario { get; set; } = "Saludable"; // Calculado del último monitoreo
        public List<TrazabilidadMonitoreoNode> Monitoreos { get; set; } = new List<TrazabilidadMonitoreoNode>();
    }

    public class TrazabilidadMonitoreoNode
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
    }
}
