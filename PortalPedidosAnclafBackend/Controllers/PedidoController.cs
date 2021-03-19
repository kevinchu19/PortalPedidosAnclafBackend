using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public PedidoController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> Post([FromBody] PedidoDTO json)
        {
            var pedido = Mapper.Map<PedidoDTO, Pedido>(json);
            
            await Repository.Pedidos.Add(pedido);
            int PostPedidoOk = await Repository.Complete();

            if (PostPedidoOk > 0)
            {
                return Ok(Mapper.Map<Pedido, PedidoDTO>(pedido));
            }
            else
            {
                return BadRequest();
            }
            
        }


        [HttpGet]
        public async Task<ActionResult<PedidoDTO>> Get(string idCliente, int skip, int take)
        {
            var pedidos = Mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoDTO> >
                (await Repository.Pedidos.GetByIdCliente(idCliente, skip, take));

            return Ok(pedidos);
        }

    }
}
