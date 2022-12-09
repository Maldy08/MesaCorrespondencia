using Microsoft.JSInterop;
using System.IO;

namespace MesaCorrespondencia.Client.Services
{
    public interface IGCloudService
    {

        public Task<byte[]> GetDocumento(VwOficiosLista oficio);
        public Task<String> GetConsecutivo(String depto);

    }
}
