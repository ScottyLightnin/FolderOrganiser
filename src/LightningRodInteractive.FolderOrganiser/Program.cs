using System.Reflection;

var currentYear = DateTime.Now.Year;
foreach(var file in Directory.EnumerateFiles(Environment.CurrentDirectory))
{
    if (Path.GetFileName(file).StartsWith(Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty))
    {
        continue;
    }
    var modifiedTime = File.GetLastWriteTime(file);
    bool shouldArchive = modifiedTime.Year < currentYear;
    var folderRoot = string.Empty;
    if (shouldArchive)
    {
        CreateFolder("Archive");
        folderRoot = "Archive";
    }
    var year = modifiedTime.Year.ToString();
    CreateFolder(Path.Combine(folderRoot, year));
    var month = modifiedTime.Month.ToString("D2");
    var destinationFolder = Path.Combine(folderRoot, year, month);
    CreateFolder(destinationFolder);
    File.Move(file, Path.Combine(destinationFolder, Path.GetFileName(file)));
}

void CreateFolder(string folderName)
{
    if (!Directory.Exists(folderName))
    {
        Directory.CreateDirectory(folderName);
    }
}

