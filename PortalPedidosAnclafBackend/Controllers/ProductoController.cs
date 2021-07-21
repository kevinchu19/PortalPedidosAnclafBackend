using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Helpers.Response;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetByTerminoForOrder(string termino, int skip, int take, string listaPrecios, string cliente)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            string tipoUsuario = identity.FindFirst("cliente").Value == "" ? "V" : "C";

            return  Ok(await Repository.Productos.GetByTerminoForOrder(termino, skip, take, listaPrecios, cliente, tipoUsuario));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Get(string id, string listaPrecios, string grupoBonificacion)
        {
            return Ok(await Repository.Productos.GetByIdYListaPrecios(id, listaPrecios, grupoBonificacion));
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Producto>>> Post([FromBody] Producto producto)
        {
            Producto productoEncontrado = await Repository.Productos.Get(producto.Id);

            if (productoEncontrado != null)
            {
                return BadRequest(new BaseResponse<Producto>("Bad request","El registro ya existe."));
            }
            
            await Repository.Productos.Add(producto);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Producto>("Registro generado con éxito", producto));
            }
            else
            {
                return BadRequest(new BaseResponse<Producto>("Bad request", "Ocurrió un error al dar de alta el registro"));
            }


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Producto>>> Put(string id, [FromBody] Producto producto)
        {
            Producto productoEncontrado = await Repository.Productos.Get(id);

            if (productoEncontrado!=null)
            {
                Repository.Productos.Detach(productoEncontrado);
                Repository.Productos.Update(producto);

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Producto>("Registro actualizado con éxito", producto));
                }
                else
                {
                    return BadRequest(new BaseResponse<Producto>("Error","Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Producto>("Not Found","No se encontró el producto"));


        }
    }

}
