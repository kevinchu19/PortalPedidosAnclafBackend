﻿using AutoMapper;
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
        Task DoWork();
    }

    internal class PostearPedidoEnSoftlandService : IScopedProcessingService
    {
        
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

        public async Task DoWork()
        {

            IEnumerable<PedidoDTO> pedidos = _mapper.Map<IEnumerable<Pedido>,IEnumerable<PedidoDTO>>(await _repository.Pedidos.GetForSoftland(0, Convert.ToInt32(_configuration["HostSoftland:CantidadPedidosPorProceso"])));

            if (pedidos.Count() == 0)
            {
                return;
            }
            string pedidosString = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });

            _logger.Information($"{ pedidosString }");

            ApiSoftlandResponse[] content = new ApiSoftlandResponse[] { };
            HttpResponseMessage stringTask = new HttpResponseMessage();
            try
            {
                stringTask = await client.PostAsync($"{_configuration["HostSoftland:BasePath"]}/api/pedido", new StringContent(pedidosString, Encoding.UTF8, "application/json"));
                var stream = await stringTask.Content.ReadAsStreamAsync();
                try
                {
                    content = await JsonSerializer.DeserializeAsync<ApiSoftlandResponse[]>(stream);
                }
                catch 
                {
                    content[0] = await JsonSerializer.DeserializeAsync<ApiSoftlandResponse>(stream);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al procesar pedido: No se obtuvo respuesta del servidor: {_configuration["HostSoftland:BasePath"]}");

                try
                {
                    stringTask = await client.PostAsync($"{_configuration["HostSoftland:BasePathSecundario"]}/api/pedido", new StringContent(pedidosString, Encoding.UTF8, "application/json"));
                    var stream = await stringTask.Content.ReadAsStreamAsync();
                    try
                    {
                        content = await JsonSerializer.DeserializeAsync<ApiSoftlandResponse[]>(stream);
                    }
                    catch
                    {
                        content[0] = await JsonSerializer.DeserializeAsync<ApiSoftlandResponse>(stream);
                    }
                }
                catch
                {
                    _logger.Error($"Error al procesar pedido: No se obtuvo respuesta del servidor: {_configuration["HostSoftland:BasePathSecundario"]}");
                    return;
                }
            }
            
            foreach ((PedidoDTO pedido, Int32 i) in pedidos.Select((pedido, i) => (pedido, i)))
            {
                string mensaje = content[i].mensaje;
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
                    //28/09/2021: Provisorio para que reintente siempre reprocesar pedidos
                    //await _repository.Pedidos.ActualizaPedidoTransferido(pedido.Id, 9);
                    //await _repository.Complete();
                     
                    _logger.Error($"({content[i].estado}) Error al procesar pedido {pedido.Id}: { mensaje}");
                    if (mensaje == "El número de formulario ya existe")
                    {
                        await _repository.Pedidos.ActualizaPedidoTransferido(pedido.Id, 1);
                        await _repository.Complete();
                        _logger.Error($"El pedido {pedido.Id} ya existe, pero se lo marca como transferido");
                    }
                }

            }
            
        }
    }
}
