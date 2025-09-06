using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace RandomCsvMastodonTooter;

public class Settings
{
    private const string SettingsFileName = "settings.json";

    //Note: the property setters must be public for JSON deserialization to work
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")] 
    public string? DataFileName { get; set; } = string.Empty;
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")] 
    public string? TemplateFileName { get; set; } = "toot-template.txt";
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")] 
    public int FieldCount { get; set; }
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")] 
    public string InstanceUrl { get; set; } = string.Empty;
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")] 
    public string Token { get; set; } = string.Empty;

    public static Settings Load()
    {
        if (!File.Exists(SettingsFileName))
            return new Settings();

        var text = File.ReadAllText(SettingsFileName);
        return JsonConvert.DeserializeObject<Settings>(text) ??
               throw new ApplicationException($"Your '{SettingsFileName}' appears to be empty or corrupt.");
    }

    public void SetValue(string setting, string value)
    {
        switch (setting.ToLower())
        {
            case "datafilename":
                DataFileName = value;
                break;
            case "templatefilename":
                TemplateFileName = value;
                break;
            case "fieldcount":
                if (!int.TryParse(value, out var fieldCount) || fieldCount < 1)
                    throw new ApplicationException("Invalid FieldCount value");
                FieldCount = fieldCount;
                break;
            case "instanceurl":
                InstanceUrl = value;
                break;
            case "token":
                Token = value;
                break;
            default:
                throw new ApplicationException($"Unknown setting '{setting}'");
        }

        Save();
    }

    private void Save()
    {
        var serialised = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(SettingsFileName, serialised);
    }
}