namespace RandomCsvMastodonTooter;

public static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var settings = Settings.Load();
            if (args is ["--set", var setting, var value])
            {
                settings.SetValue(setting, value);
                return;
            } else if (args.Length > 0)
                throw new ApplicationException("Invalid arguments");

            var csvManager = new CsvManager(settings);
            var randomLine = csvManager.GetRandomLine();
            var templateManager = new TemplateManager(settings);
            var tootContent = templateManager.GetToot(randomLine);
            //TODO implement Mastodon posting
            //var mastodonClient = new MastodonApiClient();
            //await mastodonClient.Post(settings.InstanceUrl, settings.Token, tootContent);
            csvManager.MoveToDone(randomLine);
        }
        catch (Exception ex)
        {
            var message = ex is ApplicationException ? ex.Message : ex.ToString();
            Console.WriteLine(message);
        }
    }
}