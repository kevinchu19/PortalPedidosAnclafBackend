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

    public class ListasDePrecioController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public ListasDePrecioController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<BaseResponse<Listasdeprecio>>> Post([FromBody] Listasdeprecio listasDePrecio)
        {
            Listasdeprecio listaPrecioEncontrado = await Repository.ListasDePrecio.Get(listasDePrecio.Id, listasDePrecio.Idproducto, listasDePrecio.Fecha);

            if (listaPrecioEncontrado != null)
            {
                return BadRequest(new BaseResponse<Listasdeprecio>("Bad request", "El registro ya existe"));
            }
            
            await Repository.ListasDePrecio.Add(listasDePrecio);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Listasdeprecio>("Registro generado con éxito", listasDePrecio));
            }
            else
            {
                return BadRequest(new BaseResponse<Listasdeprecio>("Bad request", "Ocurrió un error al dar de alta el registro"));
            }


        }


        [HttpPut("{idLista}/{idProducto}/{fecha}")]
        public async Task<ActionResult<BaseResponse<Listasdeprecio>>> Put(string idLista, string idProducto, DateTime fecha, [FromBody] Listasdeprecio listaPrecio)
        {
            Listasdeprecio listaPrecioEncontrado = await Repository.ListasDePrecio.Get(idLista, idProducto, fecha);

            if (listaPrecioEncontrado != null)
            {
                Repository.ListasDePrecio.Detach(listaPrecioEncontrado);
                
                Repository.ListasDePrecio.Update(listaPrecio);

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Listasdeprecio>("Registro actualizado con éxito", listaPrecio));
                }
                else
                {
                    return BadRequest(new BaseResponse<Listasdeprecio>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Listasdeprecio>("Not Found", "No se encontró la direccion"));


        }


    }
}
