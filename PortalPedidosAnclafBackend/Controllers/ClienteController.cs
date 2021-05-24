using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Helpers.Response;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public ClienteController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetByTerminoAndKeyParameter(string termino, string keyParameter, int skip, int take)
        {
            var clientes = Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteTypehead>>
                (await Repository.Clientes.GetByTerminoAndKeyParameter(termino, keyParameter , skip, take));

            return Ok(clientes);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get(string id)
        {
            return Ok(await Repository.Clientes.GetString(id));
        }


        [HttpPost]
        public async Task<ActionResult<BaseResponse<Cliente>>> Post([FromBody] Cliente cliente)
        {
            await Repository.Clientes.Add(cliente);
            
            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Cliente>("Registro generado con éxito", cliente));
            }
            else
            {
                return BadRequest(new BaseResponse<Cliente>("Ocurrió un error al dar de alta el registro"));
            }

            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Cliente>>> Put(string id, [FromBody] Cliente cliente)
        {
            Cliente clienteEncontrado = await Repository.Clientes.GetString(id);

            if (clienteEncontrado != null)
            {
                Repository.Clientes.Detach(clienteEncontrado);
                Repository.Clientes.Update(cliente);

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Cliente>("Registro actualizado con éxito", cliente));
                }
                else
                {
                    return BadRequest(new BaseResponse<Cliente>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Cliente>("Not Found", "No se encontró el cliente"));


        }
    }
}
