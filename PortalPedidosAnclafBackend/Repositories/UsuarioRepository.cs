using Microsoft.EntityFrameworkCore;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(PortalPedidosAnclaflexContext context) : base(context)
        { }
        public async Task<Usuario> GetByStringId(string id) => await Context.Set<Usuario>().Where(c => c.Id == id).FirstOrDefaultAsync();
    }
}
