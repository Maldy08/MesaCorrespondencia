using System.Net.Http.Json;

namespace MesaCorrespondencia.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public VsUsuario usuario { get; set; } = new();

        public AuthService(HttpClient http)
        {
            _http = http;
        }


        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task GetUserInfo(int id)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<VsUsuario>>("api/auth/get-user-info");
            if (response != null && response.Data != null)
                usuario = response.Data;
        }
    }
}
