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
            var pedidoItems = Mapper.Map<List<PedidoItemsDTO>, List<Pedidositem>>(json.Items);
            
            await Repository.Pedidos.Add(pedido);
            int PostHeaderOk = await Repository.Complete();

            if (PostHeaderOk > 0)
            {
                foreach (var item in pedidoItems)
                {
                    item.IdPedido = pedido.Id;
                }
                await Repository.PedidosItems.AddRange(pedidoItems);
                int PostItemsOk = await Repository.Complete();

                return Ok(pedido);
            }
            else
            {
                return BadRequest();
            }
            
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Pedido>>> Get(string id)
        {
            return Ok(await Repository.Pedidos.Get(id));
        }

    }
}
