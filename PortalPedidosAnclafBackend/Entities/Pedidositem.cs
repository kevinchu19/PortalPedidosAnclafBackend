﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Pedidositem
    {
        public int IdPedido { get; set; }
        public int Item { get; set; }
        public string IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Bonificacion1 { get; set; }
        public decimal Bonificacion2 { get; set; }
        public decimal Bonificacion3 { get; set; }
        public virtual Pedido IdPedidoNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
