using static System.Console;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

//write to a text file in the project folder
Trace.Listeners.Add(new TextWriterTraceListener(
    File.CreateText(Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.DesktopDirectory), "log.txt"))));

// text writer is buffered, so this option calls
// Flush() on a ll listeners after writing
Trace.AutoFlush = true;

Debug.WriteLine("Debug says, I am watching!");
Trace.WriteLine("Trace says, I am watching!");

ConfigurationBuilder builder = new();

builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json",
    optional: true, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();

TraceSwitch ts = new(
    displayName: "PacktSwitch",
    description: "This switch is set via a JSON config.");

configuration.GetSection("PacktSwitch").Bind(ts);

Trace.WriteLine(ts.TraceError, "Trace error");
Trace.WriteLine(ts.TraceWarning, "Trace warning");
Trace.WriteLine(ts.TraceInfo, "Trace information");
Trace.WriteLine(ts.TraceVerbose, "Trace verbose");

