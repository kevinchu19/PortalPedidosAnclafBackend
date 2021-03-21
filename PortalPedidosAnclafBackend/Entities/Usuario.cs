using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Usuario
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public string Idcliente { get; set; }
        public string Idvendedor { get; set; }
        public string Password { get; set; }

        public virtual Cliente IdClienteNavigation{ get; set; }
        public virtual Vendedores IdVendedorNavigation { get; set; }
    }
}
