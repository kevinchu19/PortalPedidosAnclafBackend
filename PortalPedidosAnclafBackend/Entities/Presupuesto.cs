using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Presupuesto
    {
        public Presupuesto()
        {
            Items = new HashSet<Presupuestositem>();
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
        public bool? RetiradeFabrica { get; set; }
        public string Observacion { get; set; }
        public string ObservacionLogistica { get; set; }
        public bool? EsBarrioCerrado { get; set; }
        public string IdVendedor { get; set; }
        public DateTime? Fecha { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool? PagoEnEfectivo { get; set; }
        public DateTime? FechaDeEntrega { get; set; }
        public Int16 Transferido { get; set; }
        public bool? Acopio { get; set; }
        public bool? DireccionModificada { get; set; }
        public string IdUsuario { get; set; }
        public string Estado { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Clientesdireccionesentrega IdEntregaNavigation { get; set; }
        public virtual Provincia ProvinciaEntregaNavigation { get; set; }
        public virtual ICollection<Presupuestositem> Items { get; set; }
        
    }
}
