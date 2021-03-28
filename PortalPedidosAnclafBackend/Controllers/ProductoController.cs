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
    public class ProductoController : ControllerBase
    {
        public IUnitOfWork Repository { get; }

        public ProductoController(IUnitOfWork repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetByTerminoForOrder(string termino, int skip, int take, string listaPrecios, string cliente)
        {
            return  Ok(await Repository.Productos.GetByTerminoForOrder(termino, skip, take, listaPrecios, cliente));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Get(string id, string listaPrecios, string grupoBonificacion)
        {
            return Ok(await Repository.Productos.GetByIdYListaPrecios(id, listaPrecios, grupoBonificacion));
        }
    }

}
