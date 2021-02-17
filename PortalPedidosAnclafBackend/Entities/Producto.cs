using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Producto
    {
        public Producto()
        {
            Listasdeprecios = new HashSet<Listasdeprecio>();
            Pedidositems = new HashSet<Pedidositem>();
        }

        public string Id { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Bonificacion { get; set; }

        public virtual ICollection<Listasdeprecio> Listasdeprecios { get; set; }
        public virtual ICollection<Pedidositem> Pedidositems { get; set; }
    }
}
