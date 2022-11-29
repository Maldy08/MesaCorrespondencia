using System.Net.Http.Json;

namespace MesaCorrespondencia.Client.Services
{
    public class DeptoService : IDeptoService
    {
        private readonly HttpClient _httpClient;

        public DeptoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DeptosUe>> GetAllDeptos()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<DeptosUe>>>("api/Deptos");
            return response.Data;
        }

        public async Task<List<GetDepartamentos>> GetDepartamentos(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<GetDepartamentos>>>
                ($"api/Functions/get-departamentos-function/{ id }");
            return response.Data;
        }
    }
}
