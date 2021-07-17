using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Services
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private Timer _timer { get; set; }

        public ConsumeScopedServiceHostedService(IServiceScopeFactory services,
            ILogger logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceScopeFactory Services { get; }
        

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information(
                "Consume Scoped Service Hosted Service running.");

            
                _logger.Information(
                    $"Scoped Processing Service is working. Count: {0}");

            try
            {
                _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            }
            catch (Exception ex)
            {

                _logger.Fatal($"Error al ejecutar el servicio {ex.Message}");
            }

        }

        private async void DoWork(object state)
        {
            //_logger.Information(
            //    "Consume Scoped Service Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedProcessingService>();
                try
                {
                    await scopedProcessingService.DoWork();
                }catch(Exception ex)
                {
                    _logger.Fatal(ex.Message);
                }
                
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.Information(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
