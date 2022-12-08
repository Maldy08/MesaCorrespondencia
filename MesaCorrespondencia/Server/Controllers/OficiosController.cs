using MesaCorrespondencia.Shared;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
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

        [HttpPost("update-pdf-path")]
        public async Task<ActionResult<ServiceResponse<Oficio>>> UpdatePdfPath(Oficio oficio)
        {
            try
            {
                var result = await _oficiosRepository.UpdatePdfPath(oficio);
                return Ok(result);
            }
            catch
            {

                return BadRequest("Ocurrio un Error al actualizar la ruta del oficio");
            }
        }

        [HttpPost("add-oficio")]
        public async Task<ActionResult<ServiceResponse<Oficio>>> Create(Oficio oficio)
        {
            //var file = Request.Form.Files[0];
            try
            {
                var result = await _oficiosRepository.CreateOficio(oficio);
                return Ok(result);
            }
            catch
            {
                return BadRequest(new ServiceResponse<Oficio> { Message = "Ocurrio un error al procesar la información" });
            }
        }

        //[HttpPost("add-oficio-new")]
        //public async Task<ActionResult<ServiceResponse<Oficio>>> AddOficio([FromForm] Oficio oficio)
        //{
        //    var files = Request.Form.Files;
        //    var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
        //    return new CreatedResult(resourcePath,files);
        //}

        [HttpPost("file-save/{ejercicio}/{eor}/{folio}")]
        public async Task<ActionResult<IList<UploadResult>>> UploadFile([FromForm] IEnumerable<IFormFile> files, [FromRoute] int ejercicio, [FromRoute] int eor, [FromRoute] int folio)
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
                            var folder = eor == 1 ? "oficios-expedidos" : "oficios-recibidos";
                            trustedFileNameForFileStorage = ejercicio.ToString() + "-" + eor.ToString() + "-" + folio.ToString() + ".pdf ";
                            //trustedFileNameForFileStorage = Path.GetRandomFileName();
                            var path = Path.Combine(env.ContentRootPath, folder,
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
            try
            {
                var result = await _oficiosRepository.CreateBitacora(oficiosBitacora);
                return Ok(result);
            }
            catch 
            {
                var result = new ServiceResponse<OficiosBitacora>
                {
                    Data = null,
                    Message = "Ocurrio un error al procesar la información",
                    Success = false
                };
               return BadRequest(result);
            }
        }

        [HttpPut("update-oficio")]
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

        [HttpGet("get-parametros/{ejercicio}")]
        public async Task<ActionResult<ServiceResponse<OficiosParametro>>> GetParametros(int ejercicio)
        {
            var result = await _oficiosRepository.GetParametros(ejercicio);
            return Ok(result);
        }

        [HttpPut("update-parametros")]
        public async Task<ActionResult<ServiceResponse<OficiosBitacora>>> UpdateParametrosXEXP(UlitimoExternoIndex ejercicio)
        {
            var result = await _oficiosRepository.UpdateParametrosXEXP(ejercicio.IdExterno);
            return Ok(result);
        }

        [HttpGet("get-pdf/{ejercicio}/{eor}/{folio}")]
        public async Task<ActionResult> GetOficiosPDF(int ejercicio, int eor, int folio)
        {
            var PDFpath = "";
            string nombre = ejercicio + "-" + eor + "-" + folio + ".pdf";
            if (eor == 1)
                PDFpath = Path.Combine(env.ContentRootPath, "oficios-expedidos/" + nombre);
            else PDFpath = Path.Combine(env.ContentRootPath, "oficios-recibidos/" + nombre);
            if (System.IO.File.Exists(PDFpath))
            {
                byte[] abc = System.IO.File.ReadAllBytes(PDFpath);
                System.IO.File.WriteAllBytes(PDFpath, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf"); 
            }
            else
            {
                return NotFound(PDFpath);            }
        }

        [HttpGet("get-oficio-by-folio/{ejercicio}/{eor}/{folio}")]
        public async Task<ActionResult<ServiceResponse<VwOficiosLista>>> GetOficioByFolio(int ejercicio, int eor, int folio)
        {
            var result = await _oficiosRepository.GetOficioByFolio(ejercicio,eor,folio);
            return Ok(result);
        }

        [HttpPost("add-oficioUsuext")]
        public async Task<ActionResult<ServiceResponse<OficiosUsuext>>> CreateOficioUsuext(OficiosUsuext oficiosUsuext)
        {
            //var file = Request.Form.Files[0];
            try
            {
                var result = await _oficiosRepository.CreateOficioUsuext(oficiosUsuext);
                return Ok(result);
            }
            catch
            {
                return BadRequest(new ServiceResponse<OficiosUsuext> { Message = "Ocurrio un error al procesar la información" });
            }
        }

        [HttpGet("get-index-userxt")]
        public ActionResult<ServiceResponse<VwOficiosLista>> GetIndexUserxt()
        {
            var result =  _oficiosRepository.GetIndexUserxt();
            return Ok(result);
        }

        [HttpDelete("delete-preoficio/{ejercicio}/{eor}/{folio}")]
        public async Task<IActionResult> DeleteAsync(int ejercicio, int eor, int folio)
        {
            var result = await _oficiosRepository.DeleteOficio(ejercicio, eor, folio);
            return Ok(result);
        }
    }
}
