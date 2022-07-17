namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Login(string user, string password);
        Task<ServiceResponse<VsUsuario>> GetUserInfo(int id);
    }
}
