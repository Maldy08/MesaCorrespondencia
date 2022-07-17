namespace MesaCorrespondencia.Client.Services
{
    public interface IAuthService
    {
        VsUsuario usuario { get; set; }
        Task<ServiceResponse<string>> Login(UserLogin request);
        Task GetUserInfo(int id);
    }
}
