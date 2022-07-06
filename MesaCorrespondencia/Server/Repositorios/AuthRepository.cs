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

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                new Claim(ClaimTypes.NameIdentifier, user.Usuario.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role,user.OficiosNivel == 9 ? "Usuario": "MC"),
                new Claim("deptoId",user.Depto.ToString()),
                new Claim("empleadoId",user.NoEmpleado.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}