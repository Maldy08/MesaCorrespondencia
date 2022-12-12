﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MesaCorrespondencia.Server.Repositorios
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthRepository(DataContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<VsUsuario>> GetUserInfo()
        {
            var response = new ServiceResponse<VsUsuario>();
            
            var usuario = await _context.VsUsuarios.
                FirstOrDefaultAsync(u => u.Usuario == GetUserId());
            if (usuario == null)
            {
                response.Success = false;
                response.Message = "Usuario no encontrado";
            }
            else
                response.Data = usuario;
            return response;
        }

        public async Task<ServiceResponse<string>> Login(string user, string password)
        {
            var response = new ServiceResponse<string>();
            var usuario = await _context.VsUsuarios.
                FirstOrDefaultAsync(u => u.Login.ToLower().Equals(user.ToLower())
                 && u.Pass.ToLower().Equals(password.ToLower()));
            if(usuario == null)
            {
                response.Success = false;
                response.Message = "Usuario y/o password incorrecto";
                response.Data = null;
            }
            else
            {
                response.Data = CreateToken(usuario);
            }

            return response;
        }

        private string CreateToken(VsUsuario user)
        {
            List<Claim> claims = new List<Claim>
            {
                //new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Usuario.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role,user.OficiosNivel == 9 ? "mc": "usuario")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                     _configuration["Jwt:Issuer"],
                     _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
