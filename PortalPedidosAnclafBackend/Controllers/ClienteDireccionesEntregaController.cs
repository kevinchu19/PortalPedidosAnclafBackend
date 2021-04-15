
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
    public class ClienteDireccionesEntregaController : ControllerBase
    {
        public IUnitOfWork Repository { get; }


        public ClienteDireccionesEntregaController(IUnitOfWork repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientesdireccionesentrega>>> GetByTerminoAndKeyParameter(string termino, string keyParameter, int skip, int take)
        {
            return Ok(await Repository.ClienteDireccionesEntrega.GetByTerminoAndKeyParameter(termino, keyParameter, skip, take));
        }

        [HttpGet("{id}/{numerocliente}")]
        public async Task<ActionResult<IEnumerable<Clientesdireccionesentrega>>> GetByIdAndKeyParameter(string id, string numeroCliente)
        {
            return Ok(await Repository.ClienteDireccionesEntrega.GetByIdAndKeyParameter(id, numeroCliente));
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Clientesdireccionesentrega>>> Post([FromBody] Clientesdireccionesentrega direccionEntrega)
        {
            await Repository.ClienteDireccionesEntrega.Add(direccionEntrega);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Clientesdireccionesentrega>("Registro generado con éxito", direccionEntrega));
            }
            else
            {
                return BadRequest(new BaseResponse<Clientesdireccionesentrega>("Ocurrió un error al dar de alta el registro"));
            }


        }
    }

}
