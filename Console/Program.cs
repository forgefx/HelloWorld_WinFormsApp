using System.IO.Compression;

Console.WriteLine("===================================== DEBUG INFO =====================================");
Console.WriteLine("Environment.CurrentDirectory: {0}", Environment.CurrentDirectory);
Console.WriteLine("Environment.SystemDirectory: {0}", Environment.SystemDirectory);
Console.WriteLine("Environment.OSVersion: {0}", Environment.OSVersion);
Console.WriteLine("Environment.UserName: {0}", Environment.UserName);
Console.WriteLine("Environment.MachineName: {0}", Environment.MachineName);
var machineName = Environment.MachineName; // Example: "AURORA-2021" vs "EC2AMAZON"
var isRunningLocally = machineName.Equals("AURORA-2021");
Console.WriteLine("isRunningLocally: {0}", isRunningLocally);
Console.WriteLine("");

Console.WriteLine("===================================== CONSTANTS (FOR TEAMCITY) =====================================");
string workDir = Environment.CurrentDirectory;
Console.WriteLine("workDir: {0}", workDir);
Console.WriteLine("");

Console.WriteLine("===================================== CONSTANTS (FOR LOCAL TESTING) =====================================");
if (isRunningLocally)
{
    workDir = @"C:\work";
    Console.WriteLine("workDir: {0}", workDir);
}
else
{
    Console.WriteLine("N/A");
}
Console.WriteLine("");

Console.WriteLine("===================================== LIST ALL FILES =====================================");
Console.WriteLine($"List all files in: {workDir}");
var files = Directory.GetFiles(workDir, "*.*", SearchOption.AllDirectories);
foreach (var file in files)
{
    Console.WriteLine($"file: {file}");
}
Console.WriteLine("");

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
Console.WriteLine("");

Console.WriteLine("=============================== STORE DATE-BASED VERSION ===============================");
Console.WriteLine($"##teamcity[setParameter name='env.HoloTrainer.Version' value='{version}']");
Console.WriteLine("");

Console.WriteLine("=============================== TARGET APP FILE NAME ===============================");
var appPrefix = "HelloTrainer";
var appVersion = version; // From preceding section.
var appExtension = ".exe";
var targetAppFileName = $"{appPrefix}.{appVersion}{appExtension}";
Console.WriteLine($"targetAppFileName: {targetAppFileName}");
Console.WriteLine("");

Console.WriteLine("=============================== LOCATE SOURCE APP FILE ===============================");
var sourceAppFile = Directory.GetFiles(workDir, "*.exe", SearchOption.AllDirectories).FirstOrDefault();
Console.WriteLine($"sourceAppFile: {sourceAppFile}");

// Get just the path to the file.
var sourceAppFilePath = Path.GetDirectoryName(sourceAppFile);
Console.WriteLine($"sourceAppFilePath: {sourceAppFilePath}");
Console.WriteLine("");

Console.WriteLine("=============================== MOVE/RENAME APP FILE ===============================");
// Copy sourceAppFileFullPath to targetAppFileName.
var targetAppFile = Path.Combine(sourceAppFilePath, targetAppFileName);
Console.WriteLine($"targetAppFile: {targetAppFile}");
File.Copy(sourceAppFile, targetAppFile, true);

// Verify that the file was copied.
var targetAppFileExists = File.Exists(targetAppFile);
Console.WriteLine($"targetAppFileExists: {targetAppFileExists}");
Console.WriteLine("");

Console.WriteLine("=============================== CREATE ZIP PACKAGE ===============================");
var fileToZip = targetAppFile;
var zipFileName = targetAppFileName.Replace(".exe", ".zip");
using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
{
    archive.CreateEntryFromFile(fileToZip, Path.GetFileName(fileToZip));
}
Console.WriteLine($"fileToZip: {fileToZip}");
Console.WriteLine($"zipFileName: {zipFileName}");
// Verify that the file was created.
var zipFileExists = File.Exists(zipFileName);
Console.WriteLine($"zipFileExists: {zipFileExists}");
Console.WriteLine("");
