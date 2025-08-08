using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Models
{
    public class PresupuestoItemsDTO
    {
        public int Item { get; set; }
        public string IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Bonificacion1 { get; set; }
        public decimal Bonificacion2 { get; set; }
        public decimal Bonificacion3 { get; set; }
        public decimal Bonificacion4 { get; set; }
        public decimal Bonificacion { get; set; }
    }
}
