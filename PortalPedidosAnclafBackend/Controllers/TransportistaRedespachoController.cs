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

    public class TransportistaRedespachoController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public TransportistaRedespachoController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transportistasredespacho>>> GetByTermino(string termino, int skip, int take)
        {
            return Ok(await Repository.TransportistaRedespacho.GetByTermino(termino, skip, take));
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Transportistasredespacho>>> Post([FromBody] Transportistasredespacho transportistasRedespacho)
        {
            await Repository.TransportistaRedespacho.Add(transportistasRedespacho);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Transportistasredespacho>("Registro generado con éxito", transportistasRedespacho));
            }
            else
            {
                return BadRequest(new BaseResponse<Transportistasredespacho>("Ocurrió un error al dar de alta el registro"));
            }


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Transportistasredespacho>>> Put(string id, [FromBody] Transportistasredespacho transportistaredespacho)
        {
            Transportistasredespacho transportistaredespachoEncontrado = await Repository.TransportistaRedespacho.Get(id);

            if (transportistaredespachoEncontrado != null)
            {
                Repository.TransportistaRedespacho.Detach(transportistaredespachoEncontrado);
                Repository.TransportistaRedespacho.Update(transportistaredespacho);

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Transportistasredespacho>("Registro actualizado con éxito", transportistaredespacho));
                }
                else
                {
                    return BadRequest(new BaseResponse<Transportistasredespacho>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Transportistasredespacho>("Not Found", "No se encontró al Transportista"));


        }

    }

}
