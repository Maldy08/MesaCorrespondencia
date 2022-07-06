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
    }
}
