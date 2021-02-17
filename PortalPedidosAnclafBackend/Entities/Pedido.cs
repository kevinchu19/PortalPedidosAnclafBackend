using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Pedido
    {
        public Pedido()
        {
            Pedidositems = new HashSet<Pedidositem>();
        }

        public int Id { get; set; }
        public string IdCliente { get; set; }
        public string IdClienteEntrega { get; set; }
        public string IdEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string PaisEntrega { get; set; }
        public string CodigoPostalEntrega { get; set; }
        public string ProvinciaEntrega { get; set; }
        public string ListaPrecios { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Clientesdireccionesentrega IdNavigation { get; set; }
        public virtual ICollection<Pedidositem> Pedidositems { get; set; }
    }
}
