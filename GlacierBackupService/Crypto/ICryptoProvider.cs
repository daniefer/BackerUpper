using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GlacierBackupService.Crypto
{
    public interface ICryptoProvider
    {
        Task<List<VerifiedFileLocation>> Encrypt(IEnumerable<VerifiedFileLocation> fileLocations, CancellationToken cancellationToken);
    }
}
