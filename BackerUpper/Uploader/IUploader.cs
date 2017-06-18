using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BackerUpper.Models;

namespace BackerUpper.Uploader
{
    public interface IUploadProvider
    {
        Task<List<VerfiedUploadLocation>> Upload(IEnumerable<VerifiedFileLocation> fileLocations, IProgress<string> progress, CancellationToken cancellationToken);
    }
}