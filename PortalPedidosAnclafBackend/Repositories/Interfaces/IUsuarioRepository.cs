using PortalPedidosAnclafBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories.Interfaces
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {
        Task<Usuario> GetByStringId(string id);
    }
}
