namespace RandomCsvMastodonTooter;

public class Settings
{
    public string InstanceUrl { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    
    public static Settings Load()
    {
        throw new NotImplementedException();
    }
}