using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesaCorrespondencia.Shared
{
    public class VwOficiosLista
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int Tipo { get; set; } = 1;
        public string NoOficio { get; set; } = string.Empty;
        public string? Pdfpath { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public DateTime FechaCaptura { get; set; } = DateTime.Now;
        public DateTime FechaAcuse { get; set; } = DateTime.Now;
        public DateTime? FechaLimite { get; set; } = DateTime.Now;
        public string RemDepen { get; set; } = string.Empty;
        public string RemSiglas { get; set; } = string.Empty;
        public string RemNombre { get; set; } = string.Empty;
        public string RemCargo { get; set; } = string.Empty;
        public string DestDepen { get; set; } = string.Empty;
        public string DestSiglas { get; set; } = string.Empty;
        public string DestNombre { get; set; } = string.Empty;
        public string DestCargo { get; set; } = string.Empty;
        public string Tema { get; set; } = string.Empty;
        public int Estatus { get; set; }
        public int? Empqentrega { get; set; }
        public string? Relacionoficio { get; set; } = string.Empty;
        public int Depto { get; set; }
        public int DeptoRespon { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreResponsable { get; set; } = string.Empty;
        public int Rol { get; set; }
    }
}
