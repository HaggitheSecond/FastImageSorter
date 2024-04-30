// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


var path = @"C:\Users\haggi\OneDrive\Bilder\Fotos\Korea Japan 24";

var files =  Directory.GetFiles(path);

var filesToDelete = files.Where(f => f.EndsWith(" 1.jpg")).ToList();

Console.WriteLine(string.Join(Environment.NewLine, filesToDelete));
Console.WriteLine();
Console.WriteLine("Deleting: " + filesToDelete.Count + " files");

foreach (var file in filesToDelete)
{
    File.Delete(file);
}

Console.WriteLine("Done");