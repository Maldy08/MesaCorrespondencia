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

        public async Task CreateOficio(Oficio oficio)
        {
            await _httpClient.PostAsJsonAsync("api/oficios",oficio);
        }

        public async Task<List<OficiosBitacora>> GetBitacorasList(int ejercicio, int folio, int eor)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OficiosBitacora>>>($"api/oficios/get-bitacora-oficio/{ejercicio}/{folio}/{eor}");

            return response.Data;
        }

        public async Task<OficiosEstatus> GetOficioEstatus(int id, int eor)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<OficiosEstatus>>($"api/oficios/get-estatus/{id}/{eor}");
            return response.Data;
        }

        public async Task<List<OficiosEstatus>> GetOficioEstatusAll()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OficiosEstatus>>>("api/oficios/get-estatus");
            return response.Data;
        }

        public async Task<List<OficiosUsuext>> GetOficioUsuextAll()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OficiosUsuext>>>("api/oficios/get-oficios-usuariosext");
            return response.Data;
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