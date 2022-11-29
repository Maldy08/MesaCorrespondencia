using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesaCorrespondencia.Shared
{

    //Tabla solamente de Folios de la mesa de correspondencia, no numeros consecutivos de oficio por departamento
    public class OficiosParametro
    {
        public int Ejercicio { get; set; }
        public int NextFRec { get; set; }
        public int NextFEnv { get; set; }
        public int NextFXexp { get; set; }
    }
}
