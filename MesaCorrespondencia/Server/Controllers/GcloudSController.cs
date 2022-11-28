using MesaCorrespondencia.Shared;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using MesaCorrespondencia.Server.Repository;
namespace MesaCorrespondencia.Server.Controllers
{
      
    [ApiController]
    [Route("[controller]")]
    public class GcloudSController : ControllerBase
    {
        private readonly IGCloudSRepository _IGCloudSRepository;
        public GcloudSController(IGCloudSRepository logger)
        {
            _IGCloudSRepository = logger;
        }

        [HttpGet]
        public MemoryStream GetData()
        {
            var g = new GCloudS(GetCredentials());
            var ms = g.DriveExportWord();
            ms.Position = 0;
            return ms;
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