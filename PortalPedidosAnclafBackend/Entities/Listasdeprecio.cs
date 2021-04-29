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
        public virtual Producto IdproductoNavigation { get; set; }
    }
}
