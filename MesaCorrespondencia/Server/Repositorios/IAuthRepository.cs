namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<string>> Login(string user, string password);
    }
}
