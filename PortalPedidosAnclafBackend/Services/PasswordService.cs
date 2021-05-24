using Microsoft.Extensions.Options;
using PortalPedidosAnclafBackend.Helpers.Options;
using PortalPedidosAnclafBackend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Services
{
    public class PasswordService : IPasswordHasher
    {
        private readonly PasswordOptions _options;

        public PasswordService( IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('-');
            if (parts.Length!=3)
            {
                return false;
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);
            
            using (var algorithm = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                var keyToCheck = algorithm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(key);
            }

        }

        public string Hash(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, _options.SaltSize, _options.Iterations))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);
                
                return $"{_options.Iterations}-{salt}-{key}";
            }
        }
    }
}
