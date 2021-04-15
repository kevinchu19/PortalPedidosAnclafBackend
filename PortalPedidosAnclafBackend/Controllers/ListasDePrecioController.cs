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
            await Repository.ListasDePrecio.Add(listasDePrecio);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Listasdeprecio>("Registro generado con éxito", listasDePrecio));
            }
            else
            {
                return BadRequest(new BaseResponse<Listasdeprecio>("Ocurrió un error al dar de alta el registro"));
            }


        }
    }
}
