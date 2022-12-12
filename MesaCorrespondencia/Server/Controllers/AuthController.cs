using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MesaCorrespondencia.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authRepository.Login(request.Usuario,request.Password);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("get-user-info")]
        public async Task<ActionResult<ServiceResponse<VsUsuario>>> GetUserInfo()
        {
            var response = await _authRepository.GetUserInfo();
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
