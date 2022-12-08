using Google.Apis.Auth.OAuth2;
using MesaCorrespondencia.Shared;

namespace MesaCorrespondencia.Server.Repository
{
    public class GCloudSRepository : IGCloudSRepository
    {
        public Task<ServiceResponse<MemoryStream>> getData()
        {
            ServiceResponse<MemoryStream> a = new ServiceResponse<MemoryStream>();

            try
            {
                var g = new GCloudS(GetCredentials(),null);
                var ms = g.DriveExportWord();
                ms.Position = 0;
                a.Data = ms;
                a.Success=true;
            }
            catch
            {
                a.Data = null;
                a.Success = false;
            }
            return Task.FromResult(a);
        }

        private GoogleCredential GetCredentials()
        {

            GoogleCredential credential;
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream);
            }
            return credential;
        }


    }
}
