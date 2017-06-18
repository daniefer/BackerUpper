using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackerUpper.Models;
using Ionic.Zip;
using Ionic.Zlib;

namespace BackerUpper.Compress
{
    public class CompressionProvider : ICompressionProvider
    {
        public CompressionProvider()
        {
        }

        public FileInfo CompressAsync(VerifiedBackupLocation backupLocation, IProgress<string> progress)
        {
            var tempFile = Path.GetTempFileName();
            var files = backupLocation.DirectoryInfo.GetFiles();
            using (ZipFile zip = new ZipFile())
            {
                zip.CompressionLevel = CompressionLevel.BestCompression;
                zip.AddProgress += (sender, args) =>
                {
                    if (args.EventType == ZipProgressEventType.Adding_AfterAddEntry)
                        progress.Report($"Adding {backupLocation.BackupFileName} files to backup. Progress: {new decimal(args.EntriesTotal) / new decimal(files.Length):P1}");
                };
                zip.SaveProgress += (sender, args) =>
                {
                    if (args.EventType == ZipProgressEventType.Saving_AfterWriteEntry)
                        progress.Report($"Saving {backupLocation.BackupFileName} files to backup. Progress: {new decimal(args.EntriesSaved) / new decimal(files.Length):P1}");
                };
                zip.AddFiles(files.Select(file => file.FullName));
                zip.Save(tempFile);
            }
            return new FileInfo(tempFile);
        }

        public async Task<List<VerifiedFileLocation>> CompressAsync(IEnumerable<VerifiedBackupLocation> backupLocations, IProgress<string> progress, CancellationToken cancellationToken)
        {
            return (await Task.WhenAll(backupLocations.Select(backup => CompressAsync(backup, progress, cancellationToken))).ConfigureAwait(false)).ToList();
        }

        public Task<VerifiedFileLocation> CompressAsync(VerifiedBackupLocation backupLocation, IProgress<string> progress, CancellationToken cancellationToken)
        {
            return Extensions.Extensions.RunAsync(() =>
            {
                var fileInfo = CompressAsync(backupLocation, progress);
                return new VerifiedFileLocation(fileInfo, backupLocation.BackupFileName);
            });

        }
    }
}
