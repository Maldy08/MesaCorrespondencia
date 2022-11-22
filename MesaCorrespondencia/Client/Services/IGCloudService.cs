namespace MesaCorrespondencia.Client.Services
{
    public interface IGCloudService
    {

        public  Task<MemoryStream> GetDocumento(VwOficiosLista oficio);

    }
}
