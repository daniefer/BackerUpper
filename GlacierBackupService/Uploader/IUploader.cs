using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GlacierBackupService.Models;

namespace GlacierBackupService
{
    public interface IUploadProvider
    {
        Task<List<VerfiedUploadLocation>> Upload(IEnumerable<VerifiedFileLocation> fileLocations, IProgress<string> progress, CancellationToken cancellationToken);
    }
}