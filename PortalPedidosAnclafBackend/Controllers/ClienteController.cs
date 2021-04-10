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
    public class ClienteController : ControllerBase
    {
        public IUnitOfWork Repository { get; }
        public IMapper Mapper { get; }

        public ClienteController(IUnitOfWork repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetByTerminoAndKeyParameter(string termino, string keyParameter, int skip, int take)
        {
            var clientes = Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteTypehead>>
                (await Repository.Clientes.GetByTerminoAndKeyParameter(termino, keyParameter , skip, take));

            return Ok(clientes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get(int id)
        {
            return Ok(await Repository.Clientes.Get(id));
        }

    }
}
