using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GlacierBackupService
{
    public interface ICompressionProvider
    {
        Task<List<VerifiedFileLocation>> CompressAsync(IEnumerable<VerifiedBackupLocation> fileInfo, CancellationToken cancellationToken);
    }
}