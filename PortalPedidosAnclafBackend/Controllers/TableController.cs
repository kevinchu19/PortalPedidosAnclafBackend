using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Helpers.Response;
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

    public class TableController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public TableController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }


        [HttpGet("pedidos")]
        public async Task<ActionResult<IEnumerable<PedidoTFDTO>>> GetPedidosForTF(string idCliente, string idVendedor, string fechaDesde,
                                                               string fechaHasta, [FromQuery] PaginationParameters parameters)
        {

            List<Pedido> pedidos = new List<Pedido>(await this.Repository.Pedidos.GetByParametersForTF(idCliente, idVendedor, fechaDesde, fechaHasta));
            List<PedidoTFDTO> response = new List<PedidoTFDTO>();
            
            pedidos.ForEach(p=> {
                response.Add(new PedidoTFDTO()
                {
                    Id = p.Id,
                    Fecha = p.Fecha.ToString("dd/MM/yyyy"),
                    DireccionEntrega = p.DireccionEntrega,
                    RazonSocial = p.Cliente.RazonSocial,
                    Importe = p.Items.Sum(item => item.Cantidad * (item.Precio - (item.Precio * item.Bonificacion / 100))).ToString("0.00")
                });
            });

            return Ok(response);
        }

    }


}
