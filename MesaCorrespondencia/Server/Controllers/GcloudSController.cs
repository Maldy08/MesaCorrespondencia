using MesaCorrespondencia.Shared;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using MesaCorrespondencia.Server.Repository;
using Microsoft.JSInterop;
using System.Text.Json;
using MesaCorrespondencia.Server.Repositorios;

namespace MesaCorrespondencia.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GcloudSController : ControllerBase
    {
        private readonly IGCloudSRepository _IGCloudSRepository;
        private readonly IOficiosRepository _IOficio;

        private readonly IWebHostEnvironment env;
        public GcloudSController(IGCloudSRepository logger, IWebHostEnvironment env, IOficiosRepository _IOficio)
        {
            _IGCloudSRepository = logger;
            this.env = env;
            this._IOficio = _IOficio;
        }

        [HttpGet("doc/{ejercicio}/{eor}/{folio}")]  //([FromQuery]VwOficiosLista preoficio)
        public async Task<IActionResult> GetDat(int ejercicio, int eor, int folio)
        {
            var preoficio = _IOficio.GetOficioByFolio(ejercicio, eor, folio).Result.Data;
            DotNetStreamReference streamRef = null;
            MemoryStream ms = null;
            if (preoficio != null)
            {
                var g = new GCloudS(GetCredentials(), preoficio);

                ms = g.DriveExportWord();
                ms.Position = 0;
                return File(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Documento.docx");
            }
            return NotFound();
        }


        [HttpGet("get-consecutivo/{dpto}")]
        public string GetData(String dpto)
        {
            if (Int32.TryParse(dpto, out int departamento))
            {
                var g = new GSheets(GetCredentials(), departamento);
                var DATA = g.ReadAsync();
                return DATA.Result.ToString();
            }
            else
            {
                return ($"error en el departamento '{dpto}'");
            }


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