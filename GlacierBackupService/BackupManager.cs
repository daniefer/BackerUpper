using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GlacierBackupService.Crypto;
using NLog;

namespace GlacierBackupService
{
    public class BackupManager : IBackupManager
    {
        private readonly ICompressionProvider _compressionProvider;
        private readonly IBackupProvider _backupProvider;
        private readonly ICryptoProvider _cryptoProvider;
        private readonly IUploadProvider _uploadProvider;
        private readonly ILogger _logger;

        public BackupManager(ICompressionProvider compressionProvider,
                             IBackupProvider backupProvider,
                             ICryptoProvider cryptoProvider,
                             IUploadProvider uploadProvider,
                             ILogger logger)
        {
            _compressionProvider = compressionProvider;
            _backupProvider = backupProvider;
            _cryptoProvider = cryptoProvider;
            _uploadProvider = uploadProvider;
            _logger = logger;
        }

        public async Task Backup(CancellationToken cancellationToken)
        {
            var backupLocations = _backupProvider.BackupDirectories();
                _logger.Trace("Directories being backed up: {0}", string.Join(", ", backupLocations.Select(file => file.FilePath)));
            var compressedLocations = await _compressionProvider.CompressAsync(backupLocations, cancellationToken).ConfigureAwait(false);
                _logger.Trace("Compressed back ups: {0}", string.Join(", ", compressedLocations.Select(file => file.FileInfo.FullName)));
            var encrypedLocations = await _cryptoProvider.Encrypt(compressedLocations, cancellationToken).ConfigureAwait(false);
            _logger.Trace("Encrypted back ups: {0}", string.Join(", ", encrypedLocations.Select(file => file.FileInfo.FullName)));
            var uploadedLocations = await _uploadProvider.Upload(encrypedLocations, new UploadProgress(_logger), cancellationToken).ConfigureAwait(false);
                _logger.Info("Files backed up: {0}", string.Join(", ", uploadedLocations.Select(file => $"ArchiveId({file.ArchiveId})|Description({file.BackupDescription})")));
            await CleanupFiles(compressedLocations.Concat(encrypedLocations), cancellationToken).ConfigureAwait(false);
        }

        private Task CleanupFiles(IEnumerable<VerifiedFileLocation> files, CancellationToken cancellationToken)
        {
            return Task.WhenAll(files.Select(file => Task.Run(() =>
            {
                if (File.Exists(file.FileInfo.FullName))
                    File.Delete(file.FileInfo.FullName);
            }, cancellationToken)));
        }

        class UploadProgress : IProgress<string>
        {
            private readonly ILogger _logger;

            public UploadProgress(ILogger logger)
            {
                _logger = logger;
            }

            public void Report(string value)
            {
                _logger.Info(value);
            }
        }
    }
}
