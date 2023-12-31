using System.Diagnostics;

namespace ConsoleApp.Services;

public interface IFileService
{
    bool SaveContentToFile(string content);
    string GetContentFromFile();
}



public class FileService(string filePath) : IFileService
{

    private readonly string _filePath = filePath;


    public bool SaveContentToFile(string content) // Skriver in user-inputen i filepathen
    {
        try
        {
            using (var sw = new StreamWriter(_filePath))
            {
                sw.WriteLine(content);
            }

            return true;
        }

        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public string GetContentFromFile() // Laser ut user-inputen i filepathen
    {
        try 
        { 
            if (File.Exists(_filePath))
            {
                using var sr = new StreamReader(_filePath);
                return sr.ReadToEnd();
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

}