using System.Net.Http.Json;

namespace MesaCorrespondencia.Client.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly HttpClient _httpClient;

        public EmpleadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VsEmpleadosSisco>> GetEmpleados()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<VsEmpleadosSisco>>>("api/Empleados");
            return response.Data;
        }

        public async  Task<List<VsEmpleadosSisco>> GetEmpleadosByDepto(int depto)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<VsEmpleadosSisco>>>($"api/Empleados/{depto}");
            return response.Data;
        }
    }
}
