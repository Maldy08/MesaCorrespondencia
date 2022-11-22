using MesaCorrespondencia.Shared;

namespace MesaCorrespondencia.Server.Repository
{
    public interface IGCloudSRepository
    {

                Task<ServiceResponse<MemoryStream>> getData();


    }

}
