using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackerUpper.Models;

namespace BackerUpper.Compress
{
    public interface ICompressionProvider
    {
        Task<List<VerifiedFileLocation>> CompressAsync(IEnumerable<VerifiedBackupLocation> fileInfo, IProgress<string> progress, CancellationToken cancellationToken);
    }
}