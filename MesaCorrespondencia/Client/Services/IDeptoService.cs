using MesaCorrespondencia.Shared;

namespace MesaCorrespondencia.Client.Services
{
    public interface IDeptoService
    {
        Task<List<DeptosUe>> GetAllDeptos();
    }
}
