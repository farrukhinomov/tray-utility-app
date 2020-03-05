using Newtonsoft.Json;

namespace UtilitiesHandler
{
    public class SettingsItem
    {
        [JsonProperty("utility-name")]
        public string UtilityName { get; set; }
    }
}