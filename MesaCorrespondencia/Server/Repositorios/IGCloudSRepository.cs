namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IGCloudSRepository
    {
        Task<ServiceResponse<MemoryStream>> getData();
    }
}
