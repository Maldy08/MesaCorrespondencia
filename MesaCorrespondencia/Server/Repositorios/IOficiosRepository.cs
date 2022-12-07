using Microsoft.AspNetCore.Components.Forms;

namespace MesaCorrespondencia.Server.Repositorios
{
    public interface IOficiosRepository
    {
        Task<ServiceResponse<Oficio>> CreateOficio(Oficio oficio);
        Task<ServiceResponse<List<VwOficiosLista>>> GetAllOficios();
        Task<ServiceResponse<List<VwOficiosLista>>> GetOficiosListaMc(int eor);
        Task<ServiceResponse<List<VwOficiosLista>>> GetOficiosListaUser(int ejercicio, int eor, int idEmpleado, int iddepto);
        Task<ServiceResponse<Oficio>> UpdateOficio(Oficio oficio);
        Task<ServiceResponse<OficiosBitacora>> CreateBitacora(OficiosBitacora oficiosBitacora);
        Task<ServiceResponse<OficiosBitacora>> UpdateBitacora(OficiosBitacora oficiosBitacora);
        Task<ServiceResponse<List<OficiosBitacora>>> GetBitacoraList(int ejercicio, int folio, int eor);
        Task<ServiceResponse<List<OficiosEstatus>>> GetEstatus();
        Task<ServiceResponse<OficiosEstatus>> GetEstatusById(int id, int eor);
        Task<ServiceResponse<List<OficiosUsuext>>> GetOficiosUsuariosExternos();
        Task<ServiceResponse<OficiosParametro>> GetParametros(int ejercicio);
        Task<ServiceResponse<OficiosParametro>> UpdateParametros(OficiosParametro oficiosParametro);
        Task<ServiceResponse<Oficio>> UpdatePdfPath(Oficio oficio);
        Task<ServiceResponse<VwOficiosLista>> GetOficioByFolio(int ejercicio, int eor, int folio);

        Task<ServiceResponse<OficiosUsuext>> CreateOficioUsuext(OficiosUsuext oficiosUsuext);
        ServiceResponse<int> GetIndexUserxt();

    }
}
