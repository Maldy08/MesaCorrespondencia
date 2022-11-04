using System;
using System.Collections.Generic;

namespace MesaCorrespondencia.Shared
{
    public  class OficiosUsuext
    {
        public OficiosUsuext()
        {

        }
        public OficiosUsuext(int idExterno, String empresa,String siglas,String nombre,String cargo,DateTime fechaCaptura, bool activo)
        {
            IdExterno = idExterno;
            Empresa = empresa;
            Siglas = siglas;
            Nombre = nombre;
            Cargo = cargo;
            FechaCaptura = fechaCaptura;
            Activo = activo;
        }

        public int IdExterno { get; set; }
        public string Empresa { get; set; } = string.Empty;
        public string Siglas { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Cargo { get; set; }
        public DateTime FechaCaptura { get; set; }
        public bool Activo { get; set; }
    }
}
