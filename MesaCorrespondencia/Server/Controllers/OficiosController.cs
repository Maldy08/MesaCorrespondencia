using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MesaCorrespondencia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficiosController : ControllerBase
    {
        private readonly IOficiosRepository _oficiosRepository;

        public OficiosController(IOficiosRepository oficiosRepository)
        {
            _oficiosRepository = oficiosRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Oficio>>> Create(Oficio oficio)
        {
            var result = await _oficiosRepository.CreateOficio(oficio);
            return Ok(result);
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
    }
}
