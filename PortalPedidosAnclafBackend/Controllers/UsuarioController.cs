using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Helpers.Response;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }
        public IConfiguration Configuration { get; }

        public UsuarioController(IUnitOfWork repository, IMapper mapper, IConfiguration configuration)
        {
            Repository = repository;
            Mapper = mapper;
            Configuration = configuration;
        }
        
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginWithJwt([FromBody] UserInfo usuario)
        {
            var _usuario = await Repository.Usuarios.GetByStringId(usuario.Id);

            if (_usuario == null)
            {
                return Unauthorized(new UserToken {
                    Token="",
                    ExpirationDate = DateTime.Now,
                    Mensaje = "El usuario no existe"
                });
            }

            if (_usuario.Password != usuario.Password)
            {
                return Unauthorized(new UserToken
                {
                    Token = "",
                    ExpirationDate = DateTime.Now,
                    Mensaje = "Contraseña incorrecta"
                });
            }

            return GenerarToken(_usuario.Id, _usuario.Idcliente, _usuario.Idvendedor, _usuario.Descripcion);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Usuario>>> Post([FromBody] Usuario usuario)
        {
            await Repository.Usuarios.Add(usuario);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Usuario>("Registro generado con éxito", usuario));
            }
            else
            {
                return BadRequest(new BaseResponse<Usuario>("Ocurrió un error al dar de alta el registro"));
            }


        }
        private IActionResult GenerarToken(string usuario, string cliente, string vendedor, string descripcion)
        {
            var key = Configuration["key"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var expiration = DateTime.UtcNow.AddMonths(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, usuario),
                    new Claim("cliente", string.IsNullOrEmpty(cliente)?"":cliente),
                    new Claim("vendedor", string.IsNullOrEmpty(vendedor)?"":vendedor),
                    new Claim("descripcion", string.IsNullOrEmpty(descripcion)?"":descripcion)
                }),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new UserToken
            {
                Token = tokenHandler.WriteToken(token),
                ExpirationDate = expiration,
                Mensaje = ""
            });

        }


    }
}
