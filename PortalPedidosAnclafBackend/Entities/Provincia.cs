using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Provincia
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Cliente> IdClienteFacturacionNavigation { get; set; }
        public virtual ICollection<Cliente> IdClienteEntregaNavigation { get; set; }
        public virtual ICollection<Clientesdireccionesentrega> IdClienteNavigation { get; set; }
    }
}
