

namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IDeptoueRepository
    {
        Task<ServiceResponse<List<DeptosUe>>>  GetAllDeptos();
        Task<ServiceResponse<DeptosUe>> GetDeptoById(int id);
    }
}
