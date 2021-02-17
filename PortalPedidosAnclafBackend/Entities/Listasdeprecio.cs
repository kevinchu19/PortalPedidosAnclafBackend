using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Listasdeprecio
    {
        public string Id { get; set; }
        public string Idproducto { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Precio { get; set; }

        public virtual Producto IdproductoNavigation { get; set; }
    }
}
