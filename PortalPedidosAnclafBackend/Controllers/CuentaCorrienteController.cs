using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

    public class CuentaCorrienteController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public CuentaCorrienteController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("pendientes")]
        [HttpGet]
        public async Task<ActionResult<ICollection<CuentaCorrientePendientesDTO>>> GetPendientesByCliente(string cliente)
        {
            ICollection<CuentaCorrientePendientesDTO> pendientes = new List<CuentaCorrientePendientesDTO>() { };

            pendientes = Mapper.Map<ICollection<CuentaCorrientePendientesDTO>>(await Repository.CuentaCorriente.GetPendientesByClienteAsync(cliente));

            return Ok(pendientes);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<ICollection<CuentaCorriente>>> GetByCliente(string cliente)
        {
            
            return Ok(await Repository.CuentaCorriente.GetByClienteAsync(cliente));
        }



        [HttpPost]
        public async Task<ActionResult<BaseResponse<CuentaCorriente>>> Post([FromBody] CuentaCorriente cuentaCorriente)
        {

            CuentaCorriente cuentaCorrienteEncontrado = await Repository.CuentaCorriente.Get(cuentaCorriente.Id);

            if (cuentaCorrienteEncontrado != null)
            {
                return BadRequest(new BaseResponse<CuentaCorriente>("Bad request", "El registro ya existe"));
            }
            
            await Repository.CuentaCorriente.Add(cuentaCorriente);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<CuentaCorriente>("Registro generado con éxito", cuentaCorriente));
            }
            else
            {
                return BadRequest(new BaseResponse<CuentaCorriente>("Bad request", "Ocurrió un error al dar de alta el registro"));
            }


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<CuentaCorriente>>> Put(int id, [FromBody] CuentaCorriente cuentaCorriente)
        {
            CuentaCorriente cuentaCorrienteEncontrado = await Repository.CuentaCorriente.Get(id);

            if (cuentaCorrienteEncontrado != null)
            {
                Repository.CuentaCorriente.Detach(cuentaCorrienteEncontrado);
                Repository.CuentaCorriente.Update(cuentaCorriente);

                if (await Repository.Complete() > 0)
                {
                    return Ok(new BaseResponse<CuentaCorriente>("Registro actualizado con éxito", cuentaCorriente));
                }
                else
                {
                    return BadRequest(new BaseResponse<CuentaCorriente>("Error", "Ocurrió un error al actualizar el registro"));
                }
            }

            return NotFound(new BaseResponse<CuentaCorriente>("Not Found", "No se encontró la cuentaCorriente"));


        }
    }
}
