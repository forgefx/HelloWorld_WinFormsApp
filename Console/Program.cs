Console.WriteLine("===================================== DEBUG INFO =====================================");
Console.WriteLine("Environment.CurrentDirectory: {0}", Environment.CurrentDirectory);
Console.WriteLine("Environment.SystemDirectory: {0}", Environment.SystemDirectory);
Console.WriteLine("Environment.OSVersion: {0}", Environment.OSVersion);
Console.WriteLine("Environment.Version: {0}", Environment.Version);
Console.WriteLine("Environment.CommandLine: {0}", Environment.CommandLine);
Console.WriteLine("Environment.UserName: {0}", Environment.UserName);
Console.WriteLine("Environment.UserDomainName: {0}", Environment.UserDomainName);
Console.WriteLine("Environment.MachineName: {0}", Environment.MachineName);
Console.WriteLine("Environment.WorkingSet: {0}", Environment.WorkingSet);
Console.WriteLine("Environment.ProcessorCount: {0}", Environment.ProcessorCount);

Console.WriteLine("=============================== CALC DATE-BASED VERSION ===============================");
var now = DateTime.UtcNow.AddHours(-4);
var year = now.Year.ToString();
var month = now.Month.ToString();
var day = now.Day.ToString();
var hour = (now.Hour * 100).ToString();
var minute = now.Minute.ToString();
var time = (now.Hour * 100) + now.Minute;
var version = $"{year}.{month}.{day}-{time}";
Console.WriteLine($"version: {version}");

Console.WriteLine("=============================== TARGET APP FILE NAME ===============================");
var appPrefix = "HelloTrainer";
var appVersion = version; // From preceding section.
var appExtension = "exe";
var targetAppFileName = $"{appPrefix}.{appVersion}{appExtension}";
Console.WriteLine($"targetAppFileName: {targetAppFileName}");

Console.WriteLine("=============================== LOCATE SOURCE APP FILE ===============================");

Console.WriteLine("=============================== MOVE/RENAME APP FILE ===============================");