namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IEmpleadosRepository
    {
        Task<ServiceResponse<List<VsEmpleadosSisco>>> GetAll();
        Task<ServiceResponse<VsEmpleadosSisco>> GetById(int id);
        Task<ServiceResponse<List<VsEmpleadosSisco>>> GetByDepto(int depto);
    }
}
