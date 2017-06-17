using System;
using System.IO;

namespace GlacierBackupService
{
    public class VerifiedFileLocation
    {
        public VerifiedFileLocation(FileInfo fileInfo, string backupFileName)
        {
            FileInfo = fileInfo;
            BackupFileName = backupFileName;
            FileUri = new Uri(fileInfo.FullName);
        }

        public VerifiedFileLocation(Uri uri, string backupFileName)
        {
            FileInfo = null;
            BackupFileName = backupFileName;
            FileUri = uri;
        }

        public VerifiedFileLocation(string uriString, string backupFileName) : this(new Uri(uriString), backupFileName) { }

        public FileInfo FileInfo { get; }
        public string BackupFileName { get; }
        public Uri FileUri { get; }
    }
}
