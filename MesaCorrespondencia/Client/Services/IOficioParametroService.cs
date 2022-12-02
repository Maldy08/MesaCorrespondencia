namespace MesaCorrespondencia.Client.Services
{
    public interface IOficioParametroService
    {
        Task<OficiosParametro> GetParametros(int ejercicio);
    }
}
