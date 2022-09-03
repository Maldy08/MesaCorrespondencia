
namespace MesaCorrespondencia.Client.Services
{
    public interface IOficioService
    {
        //List<VwOficiosLista> OficiosLista { get; set; }
       // Task<List<VwOficiosLista>> OficiosListaMC(int eor);
        Task<List<VwOficiosLista>> OficiosLista(int eor,int? ejercicio=0,int? idEmpleado=0, int? idDepto = 0);
        Task<List<OficiosEstatus>> GetOficioEstatusAll();
        Task<OficiosEstatus> GetOficioEstatus(int id, int eor);
        Task<List<OficiosUsuext>> GetOficioUsuextAll();
        Task<List<OficiosBitacora>> GetBitacorasList(int ejercicio, int folio, int eor);
        Task<bool> CreateOficio(Oficio oficio);
        Task<bool> UpdatePdfPath(Oficio oficio);
        Task<IList<UploadResult>> Upload(MultipartFormDataContent formDataContent,int ejercicio,int eor,int folio);
        Task<VwOficiosLista> GetOficioByFolio(int ejercicio, int eor, int folio);
    }
}
