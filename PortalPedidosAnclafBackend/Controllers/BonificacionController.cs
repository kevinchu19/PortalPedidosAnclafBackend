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

    public class BonificacionController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public BonificacionController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

      
        [HttpPost]
        public async Task<ActionResult<BaseResponse<Bonificacion>>> Post([FromBody] Bonificacion bonificacion)
        {

            Bonificacion bonificacionEncontrado = await Repository.Bonificaciones.Get(bonificacion.Id);

            if (bonificacionEncontrado != null)
            {
                return BadRequest(new BaseResponse<Bonificacion>("Bad request", "El registro ya existe"));
            }
            
            await Repository.Bonificaciones.Add(bonificacion);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Bonificacion>("Registro generado con éxito", bonificacion));
            }
            else
            {
                return BadRequest(new BaseResponse<Bonificacion>("Bad request", "Ocurrió un error al dar de alta el registro"));
            }


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Bonificacion>>> Put(int id, [FromBody] Bonificacion bonificacion)
        {
            Bonificacion bonificacionEncontrado = await Repository.Bonificaciones.Get(id);

            if (bonificacionEncontrado != null)
            {
                Repository.Bonificaciones.Detach(bonificacionEncontrado);
                Repository.Bonificaciones.Update(bonificacion);

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Bonificacion>("Registro actualizado con éxito", bonificacion));
                }
                else
                {
                    return BadRequest(new BaseResponse<Bonificacion>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Bonificacion>("Not Found", "No se encontró la bonificacion"));


        }
    }
}
