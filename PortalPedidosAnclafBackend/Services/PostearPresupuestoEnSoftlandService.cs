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
    internal class PostearPresupuestoEnSoftlandService : IScopedProcessingService
    {
        
        private readonly ILogger _logger;
        private static readonly HttpClient client = new HttpClient();
        public IUnitOfWork _repository { get; }
        public IMapper _mapper { get; }
        public IConfiguration _configuration { get; }

        public PostearPresupuestoEnSoftlandService(ILogger logger, IUnitOfWork repository, IMapper mapper, IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task DoWork()
        {

            IEnumerable<PresupuestoDTO> presupuestos = _mapper.Map<IEnumerable<Presupuesto>,IEnumerable<PresupuestoDTO>>(await _repository.Presupuestos.GetForSoftland(0, Convert.ToInt32(_configuration["HostSoftland:CantidadPresupuestosPorProceso"])));

            if (presupuestos.Count() == 0)
            {
                return;
            }
            string presupuestosString = JsonSerializer.Serialize(presupuestos, new JsonSerializerOptions { WriteIndented = true });

            _logger.Information($"{ presupuestosString }");

            ApiSoftlandResponse[] content = new ApiSoftlandResponse[] { };
            HttpResponseMessage stringTask = new HttpResponseMessage();
            try
            {
                stringTask = await client.PostAsync($"{_configuration["HostSoftland:BasePath"]}/api/presupuesto", new StringContent(presupuestosString, Encoding.UTF8, "application/json"));
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
            catch (Exception)
            {
                _logger.Error($"Error al procesar presupuesto: No se obtuvo respuesta del servidor: {_configuration["HostSoftland:BasePath"]}");

                try
                {
                    stringTask = await client.PostAsync($"{_configuration["HostSoftland:BasePathSecundario"]}/api/presupuesto", new StringContent(presupuestosString, Encoding.UTF8, "application/json"));
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
                    _logger.Error($"Error al procesar presupuesto: No se obtuvo respuesta del servidor: {_configuration["HostSoftland:BasePathSecundario"]}");
                    return;
                }
            }
            
            foreach ((PresupuestoDTO presupuesto, Int32 i) in presupuestos.Select((presupuesto, i) => (presupuesto, i)))
            {
                if (content[i].estado == 200)
                {
                    try 
                    {
                        await _repository.Presupuestos.ActualizaPresupuestoTransferido(presupuesto.Id, 1);
                        await _repository.Complete();
                    }
                    catch (Exception ex)
                    {
                        var a = ex.Message;       
                    }
                        
                }

                else
                { 
                    _logger.Error($"({content[i].estado}) Error al procesar presupuesto {presupuesto.Id}: { content[i].mensaje }");
                }

            }
            
        }
    }
}
