using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransportistaRedespachoController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public TransportistaRedespachoController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transportistasredespacho>>> GetByTermino(string termino, int skip, int take)
        {
            return Ok(await Repository.TransportistaRedespacho.GetByTermino(termino, skip, take));
        }

    }

}
