using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Helpers.Response;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class VendedorController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public VendedorController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<BaseResponse<Vendedores>>> Post([FromBody] Vendedores vendedor)
        {
            await Repository.Vendedores.Add(vendedor);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Vendedores>("Registro generado con éxito", vendedor));
            }
            else
            {
                return BadRequest(new BaseResponse<Vendedores>("Ocurrió un error al dar de alta el registro"));
            }


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Vendedores>>> Put(int id, [FromBody] Vendedores vendedor)
        {
            Vendedores vendedorEncontrado = await Repository.Vendedores.Get(id);

            if (vendedorEncontrado != null)
            {
                Repository.Vendedores.Detach(vendedorEncontrado);
                Repository.Vendedores.Update(vendedor);

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Vendedores>("Registro actualizado con éxito", vendedor));
                }
                else
                {
                    return BadRequest(new BaseResponse<Vendedores>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Vendedores>("Not Found", "No se encontró el vendedor"));


        }
    }
}
