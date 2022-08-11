namespace MesaCorrespondencia.Client.Services
{
    public interface IEmpleadoService
    {
        Task<List<VsEmpleadosSisco>> GetEmpleados();
        Task<List<VsEmpleadosSisco>> GetEmpleadosByDepto(int depto);
    }
}
