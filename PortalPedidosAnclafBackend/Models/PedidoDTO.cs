using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Models
{
    public class PedidoDTO
    {
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
        public string IdVendedor { get; set; }
        public string Telefono{ get; set; }
        public string Email { get; set; }
        public DateTime Fecha { get; set; }
        public Int16 RetiradeFabrica { get; set; }
        public Int16 EsBarrioCerrado { get; set; }
        public DateTime FechaDeEntrega { get; set; }
        public Int16 PagoEnEfectivo { get; set; }
        public ICollection<PedidoItemsDTO> Items { get; set; }

    }
}
