
using Microsoft.AspNetCore.Mvc;
using PortalPedidosAnclafBackend.Entities;
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
        public async Task<ActionResult<IEnumerable<Clientesdireccionesentrega>>> GetByTermino(string termino,string keyParameter, int skip, int take)
        {
            return Ok(await Repository.ClienteDireccionesEntrega.GetByKeyParameter(termino, keyParameter, skip, take));
        }

        [HttpGet("{id}/{numerocliente}")]
        public async Task<ActionResult<IEnumerable<Clientesdireccionesentrega>>> GetByIdAndKeyParameter(string id, string numeroCliente)
        {
            return Ok(await Repository.ClienteDireccionesEntrega.GetByIdAndKeyParameter(id, numeroCliente));
        }
    }

}
