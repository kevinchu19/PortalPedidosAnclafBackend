using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Provincia
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
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
        public virtual ICollection<Cliente> IdClienteFacturacionNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cliente> IdClienteEntregaNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Clientesdireccionesentrega> IdClienteNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pedido> IdPedidoNavigation{ get; set; }
    }
}
