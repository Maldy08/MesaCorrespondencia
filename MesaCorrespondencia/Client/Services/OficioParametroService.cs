using System.Net.Http.Json;

namespace MesaCorrespondencia.Client.Services
{
    public class OficioParametroService : IOficioParametroService
    {

        private readonly HttpClient _httpClient;

        public OficioParametroService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OficiosParametro> GetParametros(int ejercicio)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<OficiosParametro>>($"api/oficios/get-parametros/{ejercicio}");
            return response.Data;
        }
    }
}
