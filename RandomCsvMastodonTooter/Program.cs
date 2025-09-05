namespace RandomCsvMastodonTooter;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var settings = Settings.Load();
        var csvManager = new CsvManager(settings);
        var randomLine = csvManager.GetRandomLine();
        var templateManager = new TemplateManager();
        var tootContent = templateManager.GetToot(randomLine);
        var mastodonClient = new MastodonApiClient();
        await mastodonClient.Post(settings.InstanceUrl, settings.Token, tootContent);
        csvManager.MoveToDone(randomLine);
    }
}