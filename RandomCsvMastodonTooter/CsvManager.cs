using System.Diagnostics;

namespace RandomCsvMastodonTooter;

internal class CsvManager
{
    private readonly Settings _settings;
    private readonly string _doneFileName;

    public CsvManager(Settings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        if (string.IsNullOrEmpty(_settings.DataFileName))
            throw new ApplicationException("DataFileName not set");
        if (!File.Exists(_settings.DataFileName))
            throw new ApplicationException($"File not found: '{_settings.DataFileName}'");
        
        _doneFileName = Path.ChangeExtension(_settings.DataFileName, ".done.csv");
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

        Debug.Assert(_settings.DataFileName != null, "_settings.DataFileName != null (Checked in constructor)");
        var lineArray = GetLineArray(_settings.DataFileName);
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

    private string[] GetLineArray(string fileName)
    {
        var lines = File.ReadAllText(fileName);
        var lineArray = lines.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        return lineArray;
    }

    private void MoveDoneToData()
    {
        if (!File.Exists(_doneFileName))
            throw new ApplicationException($"File not found: '{_doneFileName}'");

        Debug.Assert(_settings.DataFileName != null, "_settings.DataFileName != null (Checked in constructor)");
        File.Delete(_settings.DataFileName);
        File.Move(_doneFileName, _settings.DataFileName);
    }

    public void MoveToDone(string line)
    {
        Debug.Assert(_settings.DataFileName != null, "_settings.DataFileName != null (Checked in constructor)");
        var sourceArray = GetLineArray(_settings.DataFileName);
        var sourceList = sourceArray.ToList();
        sourceList.Remove(line);

        var doneArray = File.Exists(_doneFileName) ? GetLineArray(_doneFileName) : [];
        var doneList = doneArray.ToList();
        doneList.Add(line);
        
        File.WriteAllLines(_settings.DataFileName, sourceList);
        File.WriteAllLines(_doneFileName, doneList);
    }
}