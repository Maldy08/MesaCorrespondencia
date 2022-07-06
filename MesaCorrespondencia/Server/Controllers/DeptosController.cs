using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MesaCorrespondencia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptosController : ControllerBase
    {
        private readonly IDeptoueRepository _deptoueRepository;

        public DeptosController(IDeptoueRepository deptoueRepository)
        {
            _deptoueRepository = deptoueRepository;
        }

        [HttpGet]

        public async Task<ActionResult<ServiceResponse<List<DeptosUe>>>> GetDeptos()
        {
            var result = await _deptoueRepository.GetAllDeptos();
            return Ok(result);
        }

        [HttpGet("{deptoId}")]
        public async Task<ActionResult<ServiceResponse<DeptosUe>>> GetProduct(int deptoId)
        {
            var result = await _deptoueRepository.GetDeptoById(deptoId);
            return Ok(result);
        }
    }
}
