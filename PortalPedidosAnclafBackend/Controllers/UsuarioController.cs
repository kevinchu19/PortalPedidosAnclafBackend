﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Helpers.Response;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using PortalPedidosAnclafBackend.Services;
using PortalPedidosAnclafBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public IPasswordHasher _passwordService { get; }

        public UsuarioController(IUnitOfWork repository, IMapper mapper, IConfiguration configuration, IPasswordHasher passwordService)
        {
            Repository = repository;
            Mapper = mapper;
            Configuration = configuration;
            _passwordService = passwordService;
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("login/renew")]
        public async Task<IActionResult> RenewToken()
        {
            
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var _usuario = await Repository.Usuarios.GetByStringId(identity.Name);

                if (_usuario == null)
                {
                    return Unauthorized(new UserToken
                    {
                        Token = "",
                        ExpirationDate = DateTime.Now,
                        Mensaje = "El usuario no existe"
                    });
                }

                if (_usuario.Activo == 0)
                {
                    return Unauthorized(new UserToken
                    {
                        Token = "",
                        ExpirationDate = DateTime.Now,
                        Mensaje = "El usuario ha sido deshabilitado"
                    });
                }

                string usuario = identity.Name;
                string cliente = identity.FindFirst("cliente").Value;
                string vendedor = identity.FindFirst("vendedor").Value;
                string descripcion = identity.FindFirst("descripcion").Value;
                return GenerarToken(usuario, cliente, vendedor, descripcion);
            }
            
            return Unauthorized(new UserToken
            {
                Token = "",
                ExpirationDate = DateTime.Now,
                Mensaje = "Token inválido"
            });


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

            if (_usuario.Activo == 0)
            {
                return Unauthorized(new UserToken
                {
                    Token = "",
                    ExpirationDate = DateTime.Now,
                    Mensaje = "El usuario ha sido deshabilitado"
                });
            }

            if (!_passwordService.Check(_usuario.Password, usuario.Password))
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
            Usuario usuarioEncontrado = await Repository.Usuarios.GetByStringId(usuario.Id);

            if (usuarioEncontrado != null)
            {
                return BadRequest(new BaseResponse<Usuario>("Bad request", "El registro ya existe"));
            }

            usuario.Password = _passwordService.Hash(usuario.Password);


            await Repository.Usuarios.Add(usuario);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Usuario>("Registro generado con éxito", usuario));
            }
            else
            {
                return BadRequest(new BaseResponse<Usuario>("Bad request", "Ocurrió un error al dar de alta el registro"));
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

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Usuario>>> Put(string id, [FromBody] Usuario usuario)
        {
            Usuario usuarioEncontrado = await Repository.Usuarios.GetByStringId(id);

            if (usuarioEncontrado != null)
            {
                Repository.Usuarios.Detach(usuarioEncontrado);
                usuario.Password = _passwordService.Hash(usuario.Password);
                Repository.Usuarios.Update(usuario);

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Usuario>("Registro actualizado con éxito", usuario));
                }
                else
                {
                    return BadRequest(new BaseResponse<Usuario>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Usuario>("Not Found", "No se encontró el usuario"));


        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPatch("changepassword/{id}")]
        public async Task<ActionResult<BaseResponse<Usuario>>> ChangePassword(string id, [FromBody] UserInfo usuarioDTO)
        {
            Usuario usuarioEncontrado = await Repository.Usuarios.GetByStringId(id);

            if (usuarioEncontrado != null)
            {
                if (!_passwordService.Check(usuarioEncontrado.Password, usuarioDTO.Password))
                {
                    return BadRequest(new BaseResponse<Usuario>("Error", "La contraseña actual ingresada es incorrecta"));    
                }
                
                usuarioEncontrado.Password = _passwordService.Hash(usuarioDTO.NewPassword);
                
                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Usuario>("Registro actualizado con éxito", usuarioEncontrado));
                }
                else
                {
                    return BadRequest(new BaseResponse<Usuario>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Usuario>("Not Found", "No se encontró el usuario"));


        }


    }
}
