using MesaCorrespondencia.Shared;

namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IFunctionsRepository
    {
        Task<ServiceResponse<List<GetDepartamentos>>> getDepartamentosF(int id);

    }
}
