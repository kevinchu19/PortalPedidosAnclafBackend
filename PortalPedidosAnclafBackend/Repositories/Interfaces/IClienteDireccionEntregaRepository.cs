using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IClienteDireccionEntregaRepository: IRepository<Clientesdireccionesentrega>
    {
        Task<IEnumerable<Clientesdireccionesentrega>> GetByKeyParameter(string termino, string keyParameter,int skip, int take);
        Task<Clientesdireccionesentrega> GetByIdAndKeyParameter(string id, string keyParameter);
    }

}
