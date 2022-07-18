using System.Net.Http.Json;

namespace MesaCorrespondencia.Client.Services
{
    public class OficioService : IOficioService
    {
        private readonly HttpClient _httpClient;
        public OficioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<VwOficiosLista>> OficiosListaMC(int eor)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<VwOficiosLista>>>($"api/oficios/mc/{eor}");
            return response.Data;

        }
        public async Task<List<VwOficiosLista>> OficiosLista(int ejercicio, int eor, int idEmpleado, int idDepto)
        {
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<VwOficiosLista>>>($"api/oficios/{ejercicio}/{eor}/{idEmpleado}/{idDepto}");
                return response.Data;
            }
        }
    }
}


//{ejercicio}/{eor}/{idEmpleado}/{idDepto}