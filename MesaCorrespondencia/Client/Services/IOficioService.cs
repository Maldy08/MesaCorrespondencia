namespace MesaCorrespondencia.Client.Services
{
    public interface IOficioService
    {
        //List<VwOficiosLista> OficiosLista { get; set; }
        Task<List<VwOficiosLista>> OficiosListaMC(int eor);
        Task<List<VwOficiosLista>> OficiosLista(int ejercicio, int eor, int idEmpleado, int idDepto);
    }
}
