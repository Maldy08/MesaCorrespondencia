using System;
using System.Collections.Generic;

namespace MesaCorrespondencia.Shared
{
    public  class DeptosUe
    {
        public int Id { get; set; }
        public int IdCea { get; set; }
        public int? IdShpoa { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool Nivel { get; set; }
        public bool Oficial { get; set; }
        public int IdReporta { get; set; }
        public int AgrupaPoa { get; set; }
        public int? Accion { get; set; }
        public int? Meta { get; set; }
        public string? Prog { get; set; }
        public int? EmpRespon { get; set; }
        public int? Agrupdir { get; set; }
        public string Prog2 { get; set; } = string.Empty;
        public int? Meta2 { get; set; }
        public int? Accion2 { get; set; }
        public bool? Cg { get; set; }
        public int? IdCfg { get; set; }
        public string IdCp { get; set; } = string.Empty;
        public int? Ano { get; set; }
    }
}
