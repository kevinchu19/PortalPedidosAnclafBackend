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
