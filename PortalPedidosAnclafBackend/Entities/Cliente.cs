using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Clientesdireccionesentregas = new HashSet<Clientesdireccionesentrega>();
            Pedidos = new HashSet<Pedido>();
        }

        public string Id { get; set; }
        public string RazonSocial { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string DireccioFacturacion { get; set; }
        public string PaisFacturacion { get; set; }
        public string CodigoPostalFacturacion { get; set; }
        public string ProvinciaFacturacion { get; set; }
        public string DireccionEntrega { get; set; }
        public string PaisEntrega { get; set; }
        public string CodigoPostalEntrega { get; set; }
        public string ProvinciaEntrega { get; set; }
        public string ListaPrecios { get; set; }
        public virtual ICollection<Clientesdireccionesentrega> Clientesdireccionesentregas { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
