namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IOficiosRepository
    {
        //Task<ServiceResponse<List<Oficio>>> GetAllOficios();
        //Task<ServiceResponse<Oficio>> GetOficioById(int ejercicio, int folio, int eor);
        Task<ServiceResponse<Oficio>> CreateOficio(Oficio oficio);
        Task<ServiceResponse<List<VwOficiosLista>>> GetAllOficios();
        Task<ServiceResponse<List<VwOficiosLista>>> GetOficiosListaMc(int eor);
        Task<ServiceResponse<List<VwOficiosLista>>> GetOficiosListaUser(int ejercicio, int eor, int idEmpleado, int iddepto);
        Task<ServiceResponse<Oficio>> UpdateOficio(Oficio oficio);
        Task<ServiceResponse<OficiosBitacora>> CreateBitacora(OficiosBitacora oficiosBitacora);
        Task<ServiceResponse<OficiosBitacora>> UpdateBitacora(OficiosBitacora oficiosBitacora);

        Task<ServiceResponse<List<OficiosEstatus>>> GetEstatus();
        Task<ServiceResponse<OficiosEstatus>> GetEstatusById(int id, int eor);

    }
}
