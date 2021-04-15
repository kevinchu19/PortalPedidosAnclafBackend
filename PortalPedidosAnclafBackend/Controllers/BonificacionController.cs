﻿using AutoMapper;
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

    public class BonificacionController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public BonificacionController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

      
        [HttpPost]
        public async Task<ActionResult<BaseResponse<Bonificacion>>> Post([FromBody] Bonificacion bonificacion)
        {
            await Repository.Bonificaciones.Add(bonificacion);

            if (await Repository.Complete() > 0)
            {
                return Ok(new BaseResponse<Bonificacion>("Registro generado con éxito", bonificacion));
            }
            else
            {
                return BadRequest(new BaseResponse<Bonificacion>("Ocurrió un error al dar de alta el registro"));
            }


        }
    }
}
