using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Helpers.Response;
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
    
    public class PedidoController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public PedidoController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<PagedList<Pedido>>> Get(string idCliente,
                                                               string idVendedor,
                                                               string idPedido,
                                                               string fechaDesde,
                                                               string fechaHasta,
                                                               [FromQuery] PaginationParameters parameters)
        {
            var pedidos = await Repository.Pedidos.GetByParameters(idCliente, idVendedor, idPedido, fechaDesde, fechaHasta, parameters);

            return Ok(pedidos);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetById(int id)
        {
            var pedido = await Repository.Pedidos.GetById(id);

            return Ok(pedido);
        }

        [HttpPatch]
        public async Task<ActionResult<Pedido>> Patch(int idPedido, string estado)
        {

            Pedido pedidoEncontrado = await Repository.Pedidos.Get(idPedido);

            if (pedidoEncontrado != null)
            {
                Repository.Pedidos.Detach(pedidoEncontrado);
                
                Pedido pedido = await Repository.Pedidos.ActualizarEstado(idPedido, estado);
                
                await Repository.Complete();
                
                return Ok(Mapper.Map<Pedido, PedidoDTO>(pedido));

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Pedido>("Registro actualizado con éxito", pedido));
                }
                else
                {
                    return BadRequest(new BaseResponse<Pedido>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Pedido>("Not Found", "No se encontró el pedido"));

            
            
        }
    }
}
