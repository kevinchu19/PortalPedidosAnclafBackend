using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Pedido
    {
        public Pedido()
        {
            Items = new HashSet<Pedidositem>();
        }
       
        public int Id { get; set; }
        public string IdCliente { get; set; }
        public string IdClienteEntrega { get; set; }
        public string IdEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string PaisEntrega { get; set; }
        public string CodigoPostalEntrega { get; set; }
        public string ProvinciaEntrega { get; set; }
        public string ListaPrecios { get; set; }
        public string TransportistaRedespacho { get; set; }
        public string Observacion { get; set; }
        public string ObservacionLogistica { get; set; }
        public Int16 RetiradeFabrica { get; set; }
        public Int16 EsBarrioCerrado { get; set; }
        public string IdVendedor { get; set; }
        public DateTime Fecha { get; set; }
        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Clientesdireccionesentrega IdNavigation { get; set; }
        public virtual ICollection<Pedidositem> Items { get; set; }
    }
}
