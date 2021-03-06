﻿using System.IO;
using BackerUpper.Configuration;

namespace BackerUpper.Models
{
    public class VerifiedBackupLocation : BackupLocation
    {
        public VerifiedBackupLocation(BackupLocation backupLocation) : base(backupLocation.FilePath, backupLocation.BackupFileName)
        {
            this.DirectoryInfo = new DirectoryInfo(backupLocation.FilePath);
        }

        public DirectoryInfo DirectoryInfo { get; set; }
    }
}
