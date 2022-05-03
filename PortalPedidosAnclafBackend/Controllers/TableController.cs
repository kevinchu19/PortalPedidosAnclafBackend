﻿using AutoMapper;
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
                    Importe = String.Format("{0:n}", p.Items.Sum(item => item.Cantidad * (item.Precio - (item.Precio * item.Bonificacion / 100))))
                });
            });

            return Ok(response);
        }


        [HttpGet("cuentacorriente")]
        public async Task<ActionResult<ICollection<object>>> GetCuentaCorrienteByCliente(string cliente, string idVendedor, string fechaDesde,string fechaHasta)
        {
            //ICollection<object> cuentaCorriente = new List<object>() { };

            //cuentaCorriente = Mapper.Map<ICollection<object>>(await Repository.CuentaCorriente.GetByClienteAsync(cliente, idVendedor, fechaDesde, fechaHasta));
            var cuentaCorriente = await Repository.CuentaCorriente.GetByClienteAsync(cliente, idVendedor, fechaDesde, fechaHasta);
            return Ok(cuentaCorriente);
        }


        [HttpGet("cuentacorriente/pendientes")]
        public async Task<ActionResult<ICollection<CuentaCorrienteDTO>>> GetCuentaCorrientePendientesByCliente(string cliente,string idVendedor, string fechaDesde, string fechaHasta)
        {
            ICollection<CuentaCorrienteDTO> pendientes = new List<CuentaCorrienteDTO>() { };
            
            pendientes = Mapper.Map<ICollection<CuentaCorrienteDTO>>(await Repository.CuentaCorriente.GetPendientesByClienteAsync(cliente, idVendedor, fechaDesde, fechaHasta));

            return Ok(pendientes);
        }

    }


}
