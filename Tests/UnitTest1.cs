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
    }

    /// <summary>
    /// Goal:
    /// Input:
    /// Ouput: 
    /// </summary>
    [Test]
    public void RenameHelloWorldApp()
    {
        string workDir = @"%teamcity.build.checkoutDir%";
        var fullFilePath = Directory.GetFiles(workDir, "*.exe", SearchOption.AllDirectories).FirstOrDefault();
        
        
        
        

        Assert.Fail();
    }
    
    [Test]
    public void RenameAppxFile()
    {
        Console.WriteLine("================== BEGIN ==================");

        var input          = "CBRND HoloTrainer_2022.6.18.1646_ARM.appx";
        var expectedOutput = "HoloTrainer_2022.6.18-1646.appx";

        var output = input;
        
        // Strip out "CBRND " from the beginning of the string.
        output = output.Replace("CBRND ", "");
        
        // Strip out "_ARM.appx" from the end of the string.
        output = output.Replace("_ARM.appx", "");
        
        // Get last chunk of string, separated by, and including, "."
        var oldLastChunk = "." + output.Split('.').Last();
        
        // Replace old last chunk with new last chunk.
        var newLastChunk = "-" + output.Split('.').Last();
        output = output.Replace(oldLastChunk, newLastChunk);
        
        // Restore .appx extension.
        output = output + ".appx";

        // Verify values are as expected.
        Console.WriteLine($"{input} (input)");
        Console.WriteLine($"{expectedOutput} (expectedOutput)");
        Console.WriteLine($"{output} (actualOutput)");
        
        Console.WriteLine("================== END ==================");
        
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