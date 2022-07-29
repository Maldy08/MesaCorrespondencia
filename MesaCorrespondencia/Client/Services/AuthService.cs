using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace MesaCorrespondencia.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public VsUsuario usuario { get; set; } = new();

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _http = http;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }


        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task GetUserInfoDB()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<VsUsuario>>("api/auth/get-user-info");
            if (response != null && response.Data != null)
                usuario = response.Data;
            await SetUserInfoLocal();
        }

        public async Task<bool> IsUserAuthenticated() => (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;

        public async Task<bool> IsUserInRoleMc() => (await _authStateProvider.GetAuthenticationStateAsync()).User.IsInRole("mc");

        public async Task SetUserInfoLocal()
        {
            await _localStorage.SetItemAsync("depto", usuario.Depto.ToString());
            await _localStorage.SetItemAsync("depto-descripcion", usuario.DeptoDescripcion);
            await _localStorage.SetItemAsync("puesto", usuario.IdPue);
            await _localStorage.SetItemAsync("puesto-descripcion", usuario.Descripcion);
            await _localStorage.SetItemAsync("empleado", usuario.NoEmpleado);
            await _localStorage.SetItemAsync("nivel", usuario.OficiosNivel);
        }

        public async Task ClearUserInfo()
        {
            await _localStorage.RemoveItemAsync("depto");
            await _localStorage.RemoveItemAsync("depto-descripcion");
            await _localStorage.RemoveItemAsync("puesto");
            await _localStorage.RemoveItemAsync("puesto-descripcion");
            await _localStorage.RemoveItemAsync("empleado");
            await _localStorage.RemoveItemAsync("nivel");
        }
    }
}
