using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Models
{
    public class ProductoDTO
    {
        public string Id { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public string TipoProducto { get; set; }
        public string Rubro1 { get; set; }
        public string Rubro2 { get; set; }
        public decimal? Bonificacion1 { get; set; }
        public decimal? Bonificacion2 { get; set; }
        public decimal? Bonificacion3 { get; set; }

    }
}
