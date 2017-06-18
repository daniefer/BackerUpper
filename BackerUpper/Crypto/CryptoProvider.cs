using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackerUpper.Models;

namespace BackerUpper.Crypto
{
    public class CryptoProvider : ICryptoProvider
    {

        public CryptoProvider()
        {
            
        }

        public Task<List<VerifiedFileLocation>> Encrypt(IEnumerable<VerifiedFileLocation> fileLocations, CancellationToken cancellationToken)
        {
            //TODO
            // Do Nothing for now
            return Task.FromResult(fileLocations.ToList());
        }
    }
}