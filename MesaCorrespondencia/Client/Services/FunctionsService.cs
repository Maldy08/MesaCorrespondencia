using System.Net.Http.Json;

namespace MesaCorrespondencia.Client.Services
{
    public class FunctionsService : IFunctionsService
    {
        private readonly HttpClient _http;

        public FunctionsService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<GetDepartamentos>> getDepartamentosF(int id)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<GetDepartamentos>>>($"api/Functions/get-departamentos-function/{id}");
            return response.Data;
        }

    }
}
