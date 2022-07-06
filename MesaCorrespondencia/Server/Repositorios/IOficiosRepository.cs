namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IOficiosRepository
    {
        //Task<ServiceResponse<List<Oficio>>> GetAllOficios();
        //Task<ServiceResponse<Oficio>> GetOficioById(int ejercicio, int folio, int eor);
        Task<ServiceResponse<Oficio>> CreateOficio(Oficio oficio);
      //  Task<Oficio> UpdateOficio(Oficio oficio);
       
    }
}
