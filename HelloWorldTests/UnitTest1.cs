using System;
using System.IO;
using System.Linq;
using NUnit.Framework;


namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        Console.WriteLine("=============================== BEGIN ===============================");
    }

    [TearDown]
    public void TearDown()
    {
        Console.WriteLine("=============================== END ===============================");
    }

    /// <summary>
    /// Goal:
    /// Input:
    /// Ouput: 
    /// </summary>
    [Test]
    public void CreateDateBasedVersion()
    {
        Console.WriteLine("=============================== CALC DATE-BASED VERSION ===============================");
        var now = DateTime.UtcNow.AddHours(-4);
        Console.WriteLine($"now: {now} (Eastern time)");
        var year = now.Year.ToString();
        Console.WriteLine($"year: {year}");
        var month = now.Month.ToString();
        Console.WriteLine($"month: {month}");
        var day = now.Day.ToString();
        Console.WriteLine($"day: {day}");
        var hour = (now.Hour * 100).ToString();
        Console.WriteLine($"hour: {hour}");
        var minute = now.Minute.ToString();
        Console.WriteLine($"minute: {minute}");
        var time = (now.Hour * 100) + now.Minute;
        Console.WriteLine($"time: {time}");
        var version = $"{year}.{month}.{day}-{time}";
        Console.WriteLine($"version: {version}");


        Assert.IsNotNull(version);
    }


    /// <summary>
    /// Example A:  FFX HelloTrainer_2022.6.18-1646.exe
    /// Example B:  HelloTrainer_2022.6.18-1646.exe
    /// </summary>
    [Test]
    public void CreateTargetFileName()
    {
        var version = "2022.6.18-1646";
        
        Console.WriteLine("=============================== TARGET FILE NAME ===============================");
        var targetFileName = "";
        var prefix = "HelloTrainer_";
        var ver = version; // From section above.
        var ext = ".exe";
        targetFileName = $"{prefix}{ver}{ext}";
        Console.WriteLine($"targetFileName: {targetFileName}");
        
        // Assertion.
        var expected = "HelloTrainer_2022.6.18-1646.exe";
        var actual = targetFileName;
        Assert.AreEqual(expected, actual);

    }

    [Test]
    public void FindExeFile()
    {
        string workDir = @"%teamcity.build.checkoutDir%";
        var fullFilePath = @"C:\foo\bar\test.exe";

        // Check to see if the directory exists.
        if (Directory.Exists(workDir))
        {
            fullFilePath = Directory.GetFiles(workDir, "*.exe", SearchOption.AllDirectories).FirstOrDefault();
        }

        // Write the full file path to console.
        Console.WriteLine($"fullFilePath: {fullFilePath}");

        Assert.IsNotEmpty(fullFilePath);
    }

    // [Test]
    // public void CreateTextFile()
    // {
    //     // Create a text file.
    //     string filePath = @"C:\temp\test.txt";
    //     File.Create(filePath).Close();
    //
    //     // Write version to console.
    //     string text = "Hello World!";
    //     File.WriteAllText(filePath, text);
    //
    //     // Read the text file.
    //     string textFromFile = File.ReadAllText(filePath);
    //     Console.WriteLine($"textFromFile: {textFromFile}");
    //     Assert.AreEqual(text, textFromFile);
    // }

    [Test]
    public void GetFileExtension()
    {
        var fileName = @"C:\foo\bar\test.exe";
        Console.WriteLine($"fileName: {fileName}");
        var extension = Path.GetExtension(fileName);
        Console.WriteLine($"extension: {extension}");
        Assert.AreEqual(".exe", extension);
    }

    [Test]
    public void RenameAppxFile()
    {
        var input = "CBRND HoloTrainer_2022.6.18.1646_ARM.appx";
        var expectedOutput = "HoloTrainer_2022.6.18-1646.appx";

        var output = input;

        // Note filename extension.
        var ext = Path.GetExtension(input);
        Console.WriteLine($"extension: {ext}");

        // Remove filename extension (.appx or .exe).
        output = output.Replace(ext, "");

        // Strip out "CBRND " from the beginning of the string.
        output = output.Replace("CBRND ", "");

        // Strip out "_ARM.appx" from the end of the string.
        output = output.Replace("_ARM", "");

        // Get last chunk of string, separated by, and including, "."
        var oldLastChunk = "." + output.Split('.').Last();

        // Replace old last chunk with new last chunk.
        var newLastChunk = "-" + output.Split('.').Last();
        output = output.Replace(oldLastChunk, newLastChunk);

        // Restore filename extension (.appx or .exe).
        output = output + ext;

        // Verify values are as expected.
        Console.WriteLine($"{input} (input)");
        Console.WriteLine($"{expectedOutput} (expectedOutput)");
        Console.WriteLine($"{output} (actualOutput)");

        // Assert that output is equal to expected output.
        Assert.AreEqual(expectedOutput, output);
    }
}


// // List all files in work directory (including sub-directories)
// string workDir = @"%teamcity.build.checkoutDir%";
// Console.WriteLine($"listing all files in workDir: {workDir}");
// var files = Directory.GetFiles(workDir, "*.*", SearchOption.AllDirectories);
// foreach (var file in files)
// {
//     Console.WriteLine($"file: {file}");
// }
//
// Console.WriteLine($"Done listing all files in workDir: {workDir}");
//
//     // Get .appx sourcePath.
// var sourcePath = Directory.GetFiles(workDir, "*.appx", SearchOption.AllDirectories).FirstOrDefault();
//
// // Get file name without path.
// string fileName = Path.GetFileName(sourcePath);
// Console.WriteLine($"fileName: {fileName}");
//
// // Copy file to workDir root directory.
// string destinationPath = Path.Combine(workDir, fileName);
// Console.WriteLine($"destinationPath: {destinationPath}");
// File.Copy(sourcePath, destinationPath);
//
// // Confirm file copy.
// Console.WriteLine($"FileCopy: {fileName} copied to {destinationPath}");
// var finalPath = Directory.GetFiles(workDir, "*.appx", SearchOption.AllDirectories).FirstOrDefault();
// Console.WriteLine($"finalPath: {finalPath}");
