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
    
    public class PresupuestoController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public PresupuestoController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<PresupuestoDTO>>> Post([FromBody] PresupuestoDTO json)
        {
            var presupuesto = Mapper.Map<PresupuestoDTO, Presupuesto>(json);

            await Repository.Presupuestos.Add(presupuesto);
            int PostPresupuestoOk = await Repository.Complete();

            if (PostPresupuestoOk > 0)
            {
                return Ok(Mapper.Map<Presupuesto, PresupuestoDTO>(presupuesto));
            }
            else
            {
                return BadRequest();
            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<PagedList<Presupuesto>>> Get(string idCliente,
                                                               string idVendedor,
                                                               string idPresupuesto,
                                                               string fechaDesde,
                                                               string fechaHasta,
                                                               [FromQuery] PaginationParameters parameters)
        {
            var presupuestos = await Repository.Presupuestos.GetByParameters(idCliente, idVendedor, idPresupuesto, fechaDesde, fechaHasta, parameters);

            return Ok(presupuestos);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Presupuesto>> GetById(int id)
        {
            var presupuesto = await Repository.Presupuestos.GetById(id);

            return Ok(presupuesto);
        }

        [HttpPatch]
        public async Task<ActionResult<Presupuesto>> Patch(int idPresupuesto, string estado)
        {

            Presupuesto presupuestoEncontrado = await Repository.Presupuestos.Get(idPresupuesto);

            if (presupuestoEncontrado != null)
            {
                Repository.Presupuestos.Detach(presupuestoEncontrado);
                
                Presupuesto presupuesto = await Repository.Presupuestos.ActualizarEstado(idPresupuesto, estado);
                
                await Repository.Complete();
                
                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<Presupuesto>("Registro actualizado con éxito", presupuesto));
                }
                else
                {
                    return BadRequest(new BaseResponse<Presupuesto>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<Presupuesto>("Not Found", "No se encontró el presupuesto"));

            
            
        }
    }
}
