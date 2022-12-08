using System.Diagnostics;
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

        public async Task<bool> CreateOficio(Oficio oficio)
        {
            var response = await _httpClient.PostAsJsonAsync("api/oficios/add-oficio", oficio);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<OficiosBitacora>> GetBitacorasList(int ejercicio, int folio, int eor)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OficiosBitacora>>>($"api/oficios/get-bitacora-oficio/{ejercicio}/{folio}/{eor}");
            return response.Data;
        }

        public async Task<VwOficiosLista> GetOficioByFolio(int ejercicio, int eor, int folio)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<VwOficiosLista>>($"api/oficios/get-oficio-by-folio/{ejercicio}/{eor}/{folio}");
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

        public async Task<List<VwOficiosLista>> OficiosLista(int eor, int? ejercicio, int? idEmpleado, int? idDepto)
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

        public async Task<bool> UpdatePdfPath(Oficio oficio)
        {
            var response = await _httpClient.PostAsJsonAsync("api/oficios/update-pdf-path", oficio);
            return response.IsSuccessStatusCode;
        }

        public async Task<IList<UploadResult>> Upload(MultipartFormDataContent formDataContent, int ejercicio, int eor, int folio)
        {
            var response = await _httpClient.PostAsync($"api/oficios/file-save/{ejercicio}/{eor}/{folio}", formDataContent);
            var newUploadResults = await response.Content.ReadFromJsonAsync<IList<UploadResult>>();
            return newUploadResults;
        }
        //cambio Alex
        public async Task<bool> UpdateOficio(Oficio oficio)
        {

            var response = await _httpClient.PutAsJsonAsync("api/oficios/update-oficio", oficio);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateOficioUsuext(OficiosUsuext oficiosUsuext)
        {
            var response = await _httpClient.PostAsJsonAsync("api/oficios/add-oficioUsuext", oficiosUsuext);
            return response.IsSuccessStatusCode;
        }
        public async Task<int> GetIndexUserxt()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<int>>("api/oficios/get-index-userxt");
            return response.Data;
        }


        public async Task<OficiosParametro> GetOficioParametro(int ejercicio)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<OficiosParametro>>($"api/oficios/get-parametros/{ejercicio}");
            return response.Data;
        }

        public async Task<bool> UpdateParametrosXEXP(UlitimoExternoIndex ejercicio)
        {
            var response = await _httpClient.PutAsJsonAsync("api/oficios/update-parametros", ejercicio);
            return response.IsSuccessStatusCode; 
        }

        public async Task<bool> DeleteOficio(int ejercicio, int eor, int folio)
        {
               var response = await _httpClient.DeleteAsync($"api/oficios/delete-preoficio/{ejercicio}/{eor}/{folio}");
                return response.IsSuccessStatusCode;
            
        }

      
    }
}


//{ejercicio}/{eor}/{idEmpleado}/{idDepto}