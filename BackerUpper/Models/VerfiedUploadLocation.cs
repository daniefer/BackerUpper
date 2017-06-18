namespace BackerUpper.Models
{
    public class VerfiedUploadLocation
    {
        public VerfiedUploadLocation(string archiveId, string backupDescription, string checksum, string uploadTime)
        {
            ArchiveId = archiveId;
            BackupDescription = backupDescription;
            Checksum = checksum;
            UploadTime = uploadTime;
        }

        public string ArchiveId { get; }
        public string BackupDescription { get; }
        public string UploadTime { get; }
        public string Checksum { get; }
    }
}
