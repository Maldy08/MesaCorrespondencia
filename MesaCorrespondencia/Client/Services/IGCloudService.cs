using Microsoft.JSInterop;
using System.IO;

namespace MesaCorrespondencia.Client.Services
{
    public interface IGCloudService
    {

        public Task<byte[]> GetDocumento(VwOficiosLista oficio);

    }
}
