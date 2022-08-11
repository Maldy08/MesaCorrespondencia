using System;
using System.Collections.Generic;

namespace MesaCorrespondencia.Shared
{
    public  class OficiosUsuext
    {
        public int IdExterno { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Cargo { get; set; }
        public DateTime FechaCaptura { get; set; }
        public bool Activo { get; set; }
    }
}
