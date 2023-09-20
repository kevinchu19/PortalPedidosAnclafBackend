using Newtonsoft.Json;
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
        public string TipoProducto { get; set; }
        public string Rubro1 { get; set; }
        public string Rubro2 { get; set; }
        public int Activo { get; set; }
        public string ClienteExclusivo { get; set; }
        public string Visibilidad { get; set; } = "T";

        public decimal Pesokg { get; set; }

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
        public virtual ICollection<Listasdeprecio> Listasdeprecios { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pedidositem> Pedidositems { get; set; }

        public virtual ICollection<Presupuestositem> Presupuestositems { get; set; }
    }
}
