using System.Net.Http.Json;

namespace MesaCorrespondencia.Client.Services
{
    public class OficioService : IOficioService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;
        public OficioService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task<List<VwOficiosLista>> OficiosLista(int eor,int? ejercicio, int? idEmpleado, int? idDepto)
        {
            if (await _authService.IsUserInRoleMc())
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<VwOficiosLista>>>($"api/oficios/mc/{eor}");
                return response.Data;
            }
            else
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<VwOficiosLista>>>($"api/oficios/{ejercicio}/{eor}/{idEmpleado}/{idDepto}");
                return response.Data;
            }
        }


        


        //public async Task<List<VwOficiosLista>> OficiosListaMC(int eor)
        //{
        //    var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<VwOficiosLista>>>($"api/oficios/mc/{eor}");
        //    return response.Data;

        //}
        //public async Task<List<VwOficiosLista>> OficiosLista(int ejercicio, int eor, int idEmpleado, int idDepto)
        //{
        //    {
        //        var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<VwOficiosLista>>>($"api/oficios/{ejercicio}/{eor}/{idEmpleado}/{idDepto}");
        //        return response.Data;
        //    }

    }
}


//{ejercicio}/{eor}/{idEmpleado}/{idDepto}