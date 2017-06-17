using System.Configuration;

namespace BackerUpper.Configuration
{
    public class BackupLocation : ConfigurationElement
    {
        public BackupLocation()
        {
            
        }

        public BackupLocation(string filePath, string backupFileName)
        {
            FilePath = filePath;
            BackupFileName = backupFileName;
        }

        [ConfigurationProperty("filePath", IsRequired = true)]
        public string FilePath
        {
            get => (string)this["filePath"];
            set => this["filePath"] = value;
        }

        [ConfigurationProperty("backupFileName", IsRequired = true)]
        public string BackupFileName
        {
            get => (string)this["backupFileName"];
            set => this["backupFileName"] = value;
        }
    }
}
