using System.Diagnostics;

namespace RandomCsvMastodonTooter;

internal class CsvManager
{
    private readonly Settings _settings;

    public CsvManager(Settings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        if (string.IsNullOrEmpty(_settings.DataFileName))
            throw new ApplicationException("DataFileName not set");
        if (!File.Exists(_settings.DataFileName))
            throw new ApplicationException($"File not found: '{_settings.DataFileName}'");
    }
    
    public string GetRandomLine()
    {
        var line = GetRandomLine(isFirstTry:true);
        return line;
    }
    
    private string GetRandomLine(bool isFirstTry)
    {
        if (!isFirstTry)
            MoveDoneToData();
        
        var lines = File.ReadAllText(_settings.DataFileName);
        var lineArray = lines.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        if (lineArray.Length == 0)
        {
            if (isFirstTry)
                return GetRandomLine(isFirstTry:false);

            throw new Exception("No lines found in data file");
        }

        var random = new Random();
        var randomIndex = random.Next(0, lineArray.Length - 1);
        return lineArray[randomIndex];
    }

    private void MoveDoneToData()
    {
        var doneFileName = $"{_settings.DataFileName}.done";
        if (!File.Exists(doneFileName))
            throw new ApplicationException($"File not found: '{doneFileName}'");

        Debug.Assert(_settings.DataFileName != null, "_settings.DataFileName != null (Checked in constructor)");
        File.Delete(_settings.DataFileName);
        File.Move(doneFileName, _settings.DataFileName);
    }

    public void MoveToDone(string randomLine)
    {
        throw new NotImplementedException();
    }
}