using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BackerUpper.Configuration
{
    public class Configuration : ConfigurationSection, IConfiguration
    {
        public Configuration()
        {
            
        }

        [ConfigurationProperty("locations", IsRequired = false, DefaultValue = null)]
        [ConfigurationCollection(typeof(BackupLocation),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ConfigurationElementCollection<BackupLocation> BackupLocations
        {
            get => (ConfigurationElementCollection<BackupLocation>) this["locations"] ?? new ConfigurationElementCollection<BackupLocation>();
            set => this["locations"] = value;
        }

        [ConfigurationProperty("uniqueClientId", IsRequired = false, DefaultValue = null)]
        public string UniqueClientId
        {
            get => (string)this["uniqueClientId"];
            set => this["uniqueClientId"] = value;
        }

        List<BackupLocation> IConfiguration.BackupLocations => BackupLocations.ToList();

        string IConfiguration.UniqueClientId
        {
            get
            {
                if (string.IsNullOrEmpty(UniqueClientId))
                    UniqueClientId = Guid.NewGuid().ToString("D");
                return UniqueClientId;
            }
        }
    }
}
