using System;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using System.Net;
using Microsoft;
using Microsoft.AspNetCore;

using File = System.IO.File;
using System.IO;

namespace MesaCorrespondencia.Client.Services
{
    public class GCloudService : IGCloudService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;




        public GCloudService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }
        public async Task<byte[]> GetDocumento(VwOficiosLista preoficio)
        {

            var file = await _httpClient.GetStreamAsync($"GcloudS/doc/{preoficio.Ejercicio}/{preoficio.Eor}/{preoficio.Folio}");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

    }
}
