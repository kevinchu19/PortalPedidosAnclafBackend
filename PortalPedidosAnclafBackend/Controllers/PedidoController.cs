using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Helpers;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<ActionResult<PagedList<Pedido>>> Get(string idCliente, 
                                                               string idVendedor, 
                                                               string idPedido,
                                                               string fechaDesde,
                                                               string fechaHasta,
                                                               [FromQuery] PaginationParameters parameters)
        {
            var pedidos = await Repository.Pedidos.GetByParameters(idCliente, idVendedor,idPedido, fechaDesde, fechaHasta, parameters);

            return Ok(pedidos);
        }

    }
}
