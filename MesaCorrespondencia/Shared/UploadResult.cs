using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesaCorrespondencia.Shared
{
    public class UploadResult
    {
        public bool Uploaded { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public int ErrorCode { get; set; }
        public string? Path { get; set; }
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
    }
}
