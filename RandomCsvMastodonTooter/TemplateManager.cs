using System.Diagnostics;

namespace RandomCsvMastodonTooter;

internal class TemplateManager
{
    private readonly Settings _settings;

    public TemplateManager(Settings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        if (string.IsNullOrEmpty(_settings.TemplateFileName))
            throw new ApplicationException("TemplateFileName not set");        
        if (!File.Exists(_settings.TemplateFileName))
            throw new ApplicationException($"File not found: '{_settings.TemplateFileName}'");
        if (_settings.FieldCount == 0)
            throw new ApplicationException("FieldCount not set");
    }

    public string GetToot(string csvLine)
    {
        var fields = csvLine.Split("\",\"", StringSplitOptions.None);
        if (fields.Length != _settings.FieldCount)
            throw new ApplicationException($"Expected {_settings.FieldCount} fields but found {fields.Length}");

        Debug.Assert(_settings.TemplateFileName != null, "_settings.TemplateFileName != null (Checked in constructor)");
        var template = File.ReadAllText(_settings.TemplateFileName);
        for (var i = 0; i < fields.Length; i++)
        {
            var fieldNumber = i + 1;
            template = template.Replace("{Field" + fieldNumber + "}", fields[i].Trim('"'));
        }

        return template;
    }
}