using System;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace MesaCorrespondencia.Client.Services
{
    public class GCloudService : IGCloudService
    {
        private readonly HttpClient _httpClient;

        public GCloudService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<MemoryStream> GetDocumento(VwOficiosLista oficio)
        {

            var data = await _httpClient.GetStreamAsync("GcloudS");

            data.Seek(0, SeekOrigin.Begin);//set position to beginning    


            return (MemoryStream)data;

        }

    }
}