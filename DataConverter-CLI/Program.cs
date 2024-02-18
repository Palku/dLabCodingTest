using CustomPeopleInput;
using dLabConverter;
using RawConsoleOutput;


var txtFiles = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.txt").ToArray();

for (int i = 0; i < txtFiles.Length; i++)
{
    Console.WriteLine($"{i + 1} : {Path.GetFileNameWithoutExtension(txtFiles[i])}");
}

while (true)
{
    Console.WriteLine("Enter the number corresponding to a file:");
    if (int.TryParse(Console.ReadLine(), out var fileNumber) && fileNumber >= 1 && fileNumber <= txtFiles.Length)
    {
        string selectedFile = txtFiles[fileNumber - 1];

        var dConverter = new DataConverter(new PeopleInput())
            .AddOutput(new ConsoleOutput())
            .AddOutput(new XmlOutput.XmlOutput($"{Path.GetFileNameWithoutExtension(selectedFile)}.xml"));

        var filePath = selectedFile;
        await using var fileStream = File.OpenRead(filePath);
        await dConverter.ProcessData(fileStream);
        break;
    }

    Console.WriteLine("Invalid input. Try again!");
}