namespace MesaCorrespondencia.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Login(UserLogin request);
    }
}
