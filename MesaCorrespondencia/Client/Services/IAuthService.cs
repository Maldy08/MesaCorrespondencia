namespace MesaCorrespondencia.Client.Services
{
    public interface IAuthService
    {
        VsUsuario usuario { get; set; }
        Task<ServiceResponse<string>> Login(UserLogin request);
        Task GetUserInfoDB();
        Task<bool> IsUserAuthenticated();
        Task<bool> IsUserInRoleMc();
        Task SetUserInfoLocal();
        Task ClearUserInfo();
        Task GetUserInfoLocal();
    }
}
