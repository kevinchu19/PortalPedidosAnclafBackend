using AutoMapper;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Models;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Services
{
    internal interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class PostearPedidoEnSoftlandService : IScopedProcessingService
    {
        private int executionCount = 0;
        private readonly ILogger _logger;
        private static readonly HttpClient client = new HttpClient();
        public IUnitOfWork _repository { get; }
        public IMapper _mapper { get; }

        public PostearPedidoEnSoftlandService(ILogger logger, IUnitOfWork repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.Information(
                    $"Scoped Processing Service is working. Count: {0}", executionCount);

                IEnumerable<PedidoDTO> pedidos = _mapper.Map<IEnumerable<Pedido>,IEnumerable<PedidoDTO>>(await _repository.Pedidos.GetForSoftland(0,5));
                string pedidosString = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });

                _logger.Information($"{ pedidosString }");
                HttpResponseMessage stringTask = await client.PostAsync("http://66.97.35.72:80/api/pedido", new StringContent(pedidosString, Encoding.UTF8, "application/json"));

                if (stringTask.StatusCode == HttpStatusCode.OK)
                {
                    _logger.Information($"{ stringTask.Content }");
                }
                else
                {
                    _logger.Error($"{ stringTask.Content }");
                }
                

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
