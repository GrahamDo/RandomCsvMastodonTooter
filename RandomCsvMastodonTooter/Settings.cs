using Newtonsoft.Json;

namespace RandomCsvMastodonTooter;

public class Settings
{
    private const string SettingsFileName = "settings.json";
    
    public string InstanceUrl { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    
    public static Settings Load()
    {
        if (!File.Exists(SettingsFileName))
            return new Settings();

        var text = File.ReadAllText(SettingsFileName);
        return JsonConvert.DeserializeObject<Settings>(text) ??
               throw new ApplicationException($"Your '{SettingsFileName}' appears to be empty or corrupt.");
    }
}