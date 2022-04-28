using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Models
{
    public class CuentaCorrienteDTO
    {
        public string Empresa { get; set; }
        public string CodigoFormulario { get; set; }
        public int NumeroFormulario{ get; set; }
        public string FormularioAplicacion { get; set; }
        public int NumeroFormularioAplicacion { get; set; }
        public string FechaMovimiento { get; set; }
        public string FechaVencimiento { get; set; }
        public string ImporteNacional { get; set; }
        public string PdfPath { get; set; }

    }
}
