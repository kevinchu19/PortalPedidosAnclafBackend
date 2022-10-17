using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        public virtual ICollection<Usuario> Usuarios { get; set; }
        [JsonIgnore]
        public virtual ICollection<CuentaCorriente> CuentaCorriente { get; set; }
    }
}
