using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace PasadenaPromo.Services
{
    public class EmailProofService(IMemoryCache cache)
    {
        private readonly IMemoryCache _cache = cache;

        public int GenerateProofCode(string emailAdress)
        {
            var random = new Random();
            int proofCode = random.Next(1000, 9999);
            StoreProofCode(emailAdress, proofCode);
            return proofCode;
        }
        private void StoreProofCode(string emailAddress, int proofCode)
        {
            _cache.Set<int>(emailAddress, proofCode, DateTimeOffset.Now.AddMinutes(30));
        }

        public bool ValidateProofCode(string emailAddress, int userCode)
        {
            if (_cache.TryGetValue<int>(emailAddress, out int proofCode) == false)
                return false;

            return userCode == proofCode;
        }
    }
}