using Newtonsoft.Json;
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
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string DireccionEntrega { get; set; }
        public string Paisentrega { get; set; }
        public string CodigoPostalEntrega { get; set; }
        public string LocalidadEntrega { get; set; }
        public string ProvinciaEntrega { get; set; }
        public string TransportistaRedespacho { get; set; }
        public int Activo { get; set; }
        public DateTime Created_At
        {
            get
            {
                return this._createdAt.HasValue
                   ? this._createdAt.Value
                   : DateTime.Now;
            }

            set { this._createdAt = value; }
        }

        private DateTime? _createdAt = null;
        [JsonIgnore]
        public virtual Cliente IdClienteNavigation { get; set; }
        [JsonIgnore]
        public Provincia ProvinciaEntregaNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
