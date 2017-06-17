using System.Collections.Generic;

namespace BackerUpper.Configuration
{
    public interface IConfiguration
    {
        List<BackupLocation> BackupLocations { get; }
        string UniqueClientId { get; }
    }
}
