using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Models
{
    public class PedidoTFDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string DireccionEntrega { get; set; }
        public string Fecha { get; set; }
        public decimal importeNacional { get; set; }
        public string Estado { get; set; }
    }
}
