using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace MesaCorrespondencia.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public VsUsuario usuario { get; set; } = new();

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }


        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task GetUserInfo()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<VsUsuario>>("api/auth/get-user-info");
            if (response != null && response.Data != null)
                usuario = response.Data;
        }

        public async Task<bool> IsUserAuthenticated() => (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;

        public async Task<bool> IsUserInRoleMc() => (await _authStateProvider.GetAuthenticationStateAsync()).User.IsInRole("mc");
        
    }
}
