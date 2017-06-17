using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;

namespace GlacierBackupService
{
    public class CompressionProvider : ICompressionProvider
    {
        public CompressionProvider()
        {
        }

        public FileInfo CompressAsync(VerifiedBackupLocation backupLocation)
        {
            var tempFile = Path.GetTempFileName();
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFiles(backupLocation.DirectoryInfo.GetFiles().Select(file => file.FullName));
                zip.Save(tempFile);
            }
            return new FileInfo(tempFile);
        }

        public async Task<List<VerifiedFileLocation>> CompressAsync(IEnumerable<VerifiedBackupLocation> backupLocations, CancellationToken cancellationToken)
        {
            return (await Task.WhenAll(backupLocations.Select(backup => CompressAsync(backup, cancellationToken))).ConfigureAwait(false)).ToList();
        }

        public Task<VerifiedFileLocation> CompressAsync(VerifiedBackupLocation backupLocation, CancellationToken cancellationToken)
        {
            return Extensions.RunAsync(() =>
            {
                var fileInfo = CompressAsync(backupLocation);
                return new VerifiedFileLocation(fileInfo, backupLocation.BackupFileName);
            });

        }
    }
}
