using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Vendedores
    {
        public Vendedores()
        {
            Usuarios = new HashSet<Usuario>();
        }
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
