using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Clientesdireccionesentrega
    {
        public Clientesdireccionesentrega()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public string IdCliente { get; set; }
        public string IdEntrega { get; set; }
        public string Descripcion { get; set; }
        public string DireccionEntrega { get; set; }
        public string Paisentrega { get; set; }
        public string CodigoPostalEntrega { get; set; }
        public string ProvinciaEntrega { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
