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

    public class ProvinciaController: ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public ProvinciaController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provincia>>> GetByTermino(string termino, int skip, int take)
        {
            return Ok(await Repository.Provincias.GetByTermino(termino, skip, take));
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Provincia>>> Post([FromBody] Provincia provincia)
        {
            await Repository.Provincias.Add(provincia);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Provincia>("Registro generado con éxito", provincia));
            }
            else
            {
                return BadRequest(new BaseResponse<Provincia>("Ocurrió un error al dar de alta el registro"));
            }


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Provincia>>> Put(string id, [FromBody] Provincia provincia)
        {
            Provincia provinciaEncontrado = await Repository.Provincias.Get(id);

            if (provinciaEncontrado != null)
            {
                Repository.Provincias.Detach(provinciaEncontrado);
                Repository.Provincias.Update(provincia);

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Provincia>("Registro actualizado con éxito", provincia));
                }
                else
                {
                    return BadRequest(new BaseResponse<Provincia>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Provincia>("Not Found", "No se encontró la provincia"));


        }
    }
}
