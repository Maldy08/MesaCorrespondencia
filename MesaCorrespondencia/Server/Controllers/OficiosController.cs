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
        public async Task<ActionResult<ServiceResponse<OficioDTO>>> Create(OficioDTO oficio)
        {
            var result = await _oficiosRepository.CreateOficio(oficio);
            return Ok(result);
        }

        [HttpGet("MC/{eor}")]
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
    }
}
