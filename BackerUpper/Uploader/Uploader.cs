using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Glacier;
using Amazon.Glacier.Model;
using Amazon.Glacier.Transfer;
using BackerUpper.Configuration;
using BackerUpper.Extensions;
using BackerUpper.Helpers;
using BackerUpper.Models;
using NLog;

namespace BackerUpper.Uploader
{
    public class Uploader : IUploadProvider
    {
        private const string PreferedVaultName = "BackerUpper";
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public Uploader(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        
        public async Task<VerfiedUploadLocation> Upload(VerifiedFileLocation fileLocation, IProgress<string> progress, CancellationToken cancellationToken)
        {
            string vaultName = await GetVaultNameAsync(cancellationToken).ConfigureAwait(false);
            string archiveDescription = fileLocation.BackupFileName;
            using (ArchiveTransferManager transferManager = new ArchiveTransferManager(Amazon.RegionEndpoint.USEast2))
            {
                UploadResult transfer = await transferManager.UploadAsync(vaultName, archiveDescription, fileLocation.FileInfo.FullName, new UploadOptions
                {
                    StreamTransferProgress = (sender, args) => progress.Report($"{archiveDescription}:{args.PercentDone}%")
                }).ConfigureAwait(false);
                if (transfer.Checksum != fileLocation.FileInfo.Checksum())
                {
                    transferManager.DeleteArchive(vaultName, transfer.ArchiveId);
                    _logger.Error(new InvalidChecksumException(), "Failed to upload backup '{0}'. Checksum mismatch.", fileLocation.BackupFileName);
                }
                return new VerfiedUploadLocation(transfer.ArchiveId, archiveDescription, transfer.Checksum, DateTime.Now.ToUniversalTime().ToString("O"));
            }
        }


        public async Task<List<VerfiedUploadLocation>> Upload(IEnumerable<VerifiedFileLocation> fileLocations, IProgress<string> progress, CancellationToken cancellationToken)
        {
            return (await Task.WhenAll(fileLocations.Select(file => Upload(file, progress, cancellationToken))).ConfigureAwait(false)).ToList();
        }
        internal async Task<string> GetVaultNameAsync(CancellationToken cancellationToken)
        {
            using (AmazonGlacierClient client = new AmazonGlacierClient(Amazon.RegionEndpoint.USEast2))
            {
                var request = new DescribeVaultRequest
                {
                    VaultName = $"{PreferedVaultName}_{_configuration.UniqueClientId}"
                };
                try
                {
                    var details = await client.DescribeVaultAsync(request, cancellationToken).ConfigureAwait(false);
                    if (!string.IsNullOrEmpty(details.VaultName))
                    {
                        return details.VaultName;
                    }
                    using (ArchiveTransferManager manager = new ArchiveTransferManager(Amazon.RegionEndpoint.USEast2))
                    {
                        manager.CreateVault(request.VaultName);
                    }
                }
                catch (Exception e)
                {
                    _logger.Info(e, $"Vault does not exist. Creating vault '{request.VaultName}' ...");
                    throw new ConfigurationErrorsException("Cannot create vault", e);
                }

                
                return request.VaultName;
            }
        }
    }
}
