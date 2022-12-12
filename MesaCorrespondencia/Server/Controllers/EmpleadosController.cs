using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MesaCorrespondencia.Server.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosRepository _empleadosRepository;

        public EmpleadosController(IEmpleadosRepository empleadosRepository)
        {
            _empleadosRepository = empleadosRepository;
        }


        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<VsEmpleadosSisco>>>> GetAll()
        {
            var result = await _empleadosRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{depto}")]
        public async Task<ActionResult<ServiceResponse<List<VsEmpleadosSisco>>>> GetByDepto(int depto)
        {
            var result = await _empleadosRepository.GetByDepto(depto);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ServiceResponse<VsEmpleadosSisco>>> GetById(int id)
        {
            var result = await _empleadosRepository.GetById(id);
            return Ok(result);
        }
    }
}
