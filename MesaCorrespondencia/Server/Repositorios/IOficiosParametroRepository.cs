namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IOficiosParametroRepository
    {
        Task<ServiceResponse<OficiosParametro>> GetParametros(int ejercicio);
        //Task<ServiceResponse<OficiosParametro>> UpdateParametros(OficiosParametro oficiosParametro);
    }
}
