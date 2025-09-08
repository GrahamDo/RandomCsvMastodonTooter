# RandomCsvMastodonTooter

This is a console app designed to be run as a bot. It uses a CSV data file of posts, with an arbitrary field definition, to put random toots to Mastodon (typically quotes). It's designed to be left unattended, because it will recycle the items once it's exhausted them.

## Prerequisites

In order to use this bot, you'll need a Mastodon Access Token with permission to <code>write:statuses</code>. You can get one of those by going to Preferences -> Development in your Mastodon account.

Note: Every field in your CSV file must be surrounded by double quotes, because the code splits it by <code>","</code>. Also, the first row of your CSV file should <i>not</i> contain column headers.

## Getting it running

Pull the code, ensure you're on the <code>main</code> branch, and build the solution for your preferred platform. I've tested it on Ubuntu x64 and Linux Mint, but it should run on Windows too, or any other platform with .NET 8 available.

Then open a terminal and run the following for each setting to configure everything:

<code>RandomCsvMastodonTooter --set &lt;setting&gt; &lt;value&gt;</code>

### Settings

* DataFileName - The name of your "input" CSV file (e.g. <code>data.csv</code>). This file must exist in the specified path
* TemplateFileName - The name of the template to use to post (see below). The default is <code>toot-template.txt</code>, and this file must exist in the specified path
* FieldCount - For validation purposes, this is the number of fields/columns in the CSV file (e.g. <code>4</code>)
* InstanceUrl - The URL of your Mastodon instance (e.g. <code>mastodon.africa</code>)
* Token - Your Mastodon Access Token

### Running

Once everything is set up, run:

<code>RandomCsvMastodonTooter</code>

If everything's okay, the tool will chose a random line from the file specified in your DataFileName setting, apply the template from the file specified in your TemplateFileName setting, and post it. You won't see any output.

Then create a cron job or Windows Scheduled Task to do that as often as you like, and you're good to go! :-)

### The Template File

The template file is the exact string you would like to post, with placeholders for the fields in the format <code>{Field1}</code>, <code>{Field2}</code>, <code>{Field3}</code>, etc.

Here's an example file, if you were using this tool as a quote bot:

```
{Field1}

- {Field2}, ({Field3}, {Field4})
```

This assumes that <code>{Field1}</code> is the quote text, <code>{Field2}</code>, <code>{Field3}</code> is the source where the quote originally appeared, and <code>{Field4}</code> is the year the source was first published.