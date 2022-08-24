using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MesaCorrespondencia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficiosController : ControllerBase
    {
        private readonly IOficiosRepository _oficiosRepository;
        private readonly IWebHostEnvironment env;
        public OficiosController(IOficiosRepository oficiosRepository, IWebHostEnvironment env)
        {
            _oficiosRepository = oficiosRepository;
            this.env = env;
        }

        [HttpPost("add-oficio")]
        public async Task<ActionResult<ServiceResponse<Oficio>>> Create(Oficio oficio)
        {
            var result = await _oficiosRepository.CreateOficio(oficio);
            return Ok(result);
        }

        [HttpPost("file-save")]
        public async Task<ActionResult<IList<UploadResult>>> UploadFile([FromForm] IEnumerable<IFormFile> files)
        {
            var maxAllowedFiles = 3;
            long maxFileSize = 1202124545;
            var filesProcessed = 0;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            List<UploadResult> uploadResults = new();
            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay =
                    WebUtility.HtmlEncode(untrustedFileName);
                if (filesProcessed < maxAllowedFiles)
                {
                    if (file.Length == 0)
                    {

                        uploadResult.ErrorCode = 1;
                    }
                    else if (file.Length > maxFileSize)
                    {

                        uploadResult.ErrorCode = 2;
                    }
                    else
                    {
                        try
                        {
                            trustedFileNameForFileStorage = Path.GetRandomFileName();
                            var path = Path.Combine(env.ContentRootPath, "Oficios",
                                trustedFileNameForFileStorage);

                            await using FileStream fs = new(path, FileMode.Create);
                            await file.CopyToAsync(fs);
         
                            uploadResult.Uploaded = true;
                            uploadResult.StoredFileName = trustedFileNameForFileStorage;
                            uploadResult.FileName = untrustedFileName;
                            uploadResult.Path = path;
                        }
                        catch (IOException ex)
                        {

                            uploadResult.ErrorCode = 3;
                        }
                    }

                    filesProcessed++;
                }
                else
                {

                    uploadResult.ErrorCode = 4;
                }

                uploadResults.Add(uploadResult);
              
            }
            return new CreatedResult(resourcePath, uploadResults);
        }
        

        [HttpPost("add-bitacora")]
        public async Task<ActionResult<ServiceResponse<OficiosBitacora>>> CreateBitacora(OficiosBitacora oficiosBitacora)
        {
            var result = await _oficiosRepository.CreateBitacora(oficiosBitacora);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Oficio>>> UpdateOficio(Oficio oficio)
        {
            var result = await _oficiosRepository.UpdateOficio(oficio);
            return Ok(result);
        }

        [HttpPut("update-bitacora")]
        public async Task<ActionResult<ServiceResponse<OficiosBitacora>>> UpdateBitacora(OficiosBitacora oficiosBitacora)
        {
            var result = await _oficiosRepository.UpdateBitacora(oficiosBitacora);
            return Ok(result);
        }

        [HttpGet("mc/{eor}")]
        public async Task<ActionResult<ServiceResponse<List<VwOficiosLista>>>> GetOficiosMC(int eor)
        {
            var result = await _oficiosRepository.GetOficiosListaMc(eor);
            return Ok(result);
        }

        [HttpGet("{ejercicio}/{eor}/{idEmpleado}/{idDepto}")]
        public async Task<ActionResult<ServiceResponse<List<VwOficiosLista>>>> GetOficiosUsuarios(int ejercicio, int eor, int idEmpleado, int idDepto)
        {
            var result = await _oficiosRepository.GetOficiosListaUser(ejercicio,eor,idEmpleado,idDepto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<VwOficiosLista>>>> GetAllOficios()
        {
            var result = await _oficiosRepository.GetAllOficios();
            return Ok(result);
        }

        [HttpGet("get-estatus")]
        public async Task<ActionResult<ServiceResponse<List<OficiosEstatus>>>> GetAllOficiosEstatus()
        {
            var result = await _oficiosRepository.GetEstatus();
            return Ok(result);
        }

        [HttpGet("get-estatus/{id}/{eor}")]

        public async Task<ActionResult<ServiceResponse<OficiosEstatus>>> GetOficioEstatusById(int id, int eor)
        {
            var result = await _oficiosRepository.GetEstatusById(id, eor);
            return Ok(result);
        }

        [HttpGet("get-oficios-usuariosext")]
        public async Task<ActionResult<ServiceResponse<List<OficiosUsuext>>>> GetOficiosUsuariosExternos()
        {
            var result = await _oficiosRepository.GetOficiosUsuariosExternos();
            return Ok(result);
        }

        [HttpGet("get-bitacora-oficio/{ejercicio}/{folio}/{eor}")]
        public async Task<ActionResult<ServiceResponse<List<OficiosBitacora>>>> GetBitacoraList(int ejercicio, int folio, int eor)
        {
            var result = await _oficiosRepository.GetBitacoraList(ejercicio, folio, eor);
            return Ok(result);
        }
    }
}
