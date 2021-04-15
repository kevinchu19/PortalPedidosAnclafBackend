using AutoMapper;
using Microsoft.Extensions.Configuration;
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
        public IConfiguration _configuration { get; }

        public PostearPedidoEnSoftlandService(ILogger logger, IUnitOfWork repository, IMapper mapper, IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.Information(
                    $"Scoped Processing Service is working. Count: {0}", executionCount);

                IEnumerable<PedidoDTO> pedidos = _mapper.Map<IEnumerable<Pedido>,IEnumerable<PedidoDTO>>(await _repository.Pedidos.GetForSoftland(0, Convert.ToInt32(_configuration["HostSoftland:CantidadPedidosPorProceso"])));
                string pedidosString = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });

                _logger.Information($"{ pedidosString }");
                HttpResponseMessage stringTask = await client.PostAsync($"{_configuration["HostSoftland:BasePath"]}/api/pedido", new StringContent(pedidosString, Encoding.UTF8, "application/json"));
                var stream = await stringTask.Content.ReadAsStreamAsync();
                var content = await JsonSerializer.DeserializeAsync<ApiSoftlandResponse[]>(stream);


                foreach ((PedidoDTO pedido, Int32 i) in pedidos.Select((pedido, i) => (pedido, i)))
                {
                    if (content[i].estado == 200)
                    {
                        try 
                        {
                            await _repository.Pedidos.ActualizaPedidoTransferido(pedido.Id, 1);
                            await _repository.Complete();
                        }
                        catch (Exception ex)
                        {
                            var a = ex.Message;
                            
                        }
                        
                    }

                    else
                    {
                        await _repository.Pedidos.ActualizaPedidoTransferido(pedido.Id, 9);
                        await _repository.Complete();
                        _logger.Error($"({content[i].estado}) Error al procesar pedido {pedido.Id}: { content[i].mensaje }");
                    }

                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
