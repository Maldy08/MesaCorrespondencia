using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MesaCorrespondencia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionsController : ControllerBase
    {
        private readonly IFunctionsRepository _functionsRepository;


        public FunctionsController(IFunctionsRepository functionsRepository)
        {
            _functionsRepository = functionsRepository;
        }

        [HttpGet("get-departamentos-function/{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetDepartamentos>>>> Get(int id)
        {
            var result = await _functionsRepository.getDepartamentosF(id);
            return Ok(result);
        }
    }
}
