using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface ITransportistaRedespachoRepository : IRepository<Transportistasredespacho>
    {
        Task<IEnumerable<Transportistasredespacho>> GetByTermino(string termino, int skip, int take);
        Task<Transportistasredespacho> Get(string id);
    }
}
