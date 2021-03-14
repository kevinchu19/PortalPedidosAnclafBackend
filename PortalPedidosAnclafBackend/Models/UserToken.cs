using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Models
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Mensaje { get; set; }
    }
}
