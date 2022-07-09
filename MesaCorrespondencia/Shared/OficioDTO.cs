using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesaCorrespondencia.Shared
{
    public  class OficioDTO
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int Tipo { get; set; }
        public string NoOficio { get; set; } = null!;
        public string? Pdfpath { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } //Fecha del documento
        public DateTime FechaCaptura { get; set; } = DateTime.Now;
        public DateTime FechaAcuse { get; set; } //Opcional, siempre y cuando el oficio pida una fecha de respuesta y unicamente aplica a eor = 1
        public DateTime? FechaLimite { get; set; }  //Opcional, siempre y cuando el oficio pida uan fecha limite
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
        public string? Relacionoficio { get; set; }
        public int Depto { get; set; }
        public int DeptoRespon { get; set; }
        
        //Necesita una dependencia de OficiosBitacora
        //Se relaciona con ejercicio,folio,eor,fechacaptura
        public OficiosBitacora OficioBitacora { get; set; } = new();
        public List<OficiosResponsable> OficiosResponsables { get; set; } = new();
    }
}
