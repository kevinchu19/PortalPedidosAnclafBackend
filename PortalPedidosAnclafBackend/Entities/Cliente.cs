using Newtonsoft.Json;
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
            Usuarios = new HashSet<Usuario>();
        }

        public string Id { get; set; }
        public string RazonSocial { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string DireccionFacturacion { get; set; }
        public string PaisFacturacion { get; set; }
        public string CodigoPostalFacturacion { get; set; }
        public string LocalidadFacturacion { get; set; }
        public string ProvinciaFacturacion { get; set; }
        public string DireccionEntrega { get; set; }
        public string PaisEntrega { get; set; }
        public string CodigoPostalEntrega { get; set; }
        public string LocalidadEntrega { get; set; }
        public string ProvinciaEntrega { get; set; }
        public string ListaPrecios { get; set; }
        public string TransportistaRedespacho { get; set; }
        public string IdVendedor { get; set; }
        public string GrupoBonificacion { get; set; }
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
        public virtual ICollection<Clientesdireccionesentrega> Clientesdireccionesentregas { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pedido> Pedidos { get; set; }
        [JsonIgnore]
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public Provincia ProvinciaFacturacionNavigation { get; set; }
        [JsonIgnore]
        public Provincia ProvinciaEntregaNavigation { get; set; }
    }
}


