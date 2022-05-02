using System.Text;
using System.Text.RegularExpressions;

namespace hakims_livs.Utils;
using Microsoft.AspNetCore.Http;


public static class FileStorage
{
    public static async Task<string> StoreFileAsync(IFormFile file, string name)
    {

        string fileName;
        try
        {
            var wwwRootPath = HostEnvironment.GetWebRootPath();
            var extension = Path.GetExtension(file.FileName);
            fileName = CreateFileName(name, extension);
            var folder = GetFolderName(fileName);
            var path = Path.Combine(wwwRootPath + folder, fileName);
            if (!Directory.Exists(wwwRootPath + folder)) Directory.CreateDirectory(wwwRootPath + folder);
            await using var fileStream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(fileStream);
        }
        
        catch
        {
            throw new Exception("Error trying to store the file");
        }

        
        return fileName;
    }


    private static string CreateFileName(string name, string extension)
    {
        return DateTime.Now.ToString("yymmssfff") + "_" + RemoveSpecialCharacters(name) + extension;;
    }

    public static void Delete(string fileName)
    {
        var folder = GetFolderName(fileName);
        var wwwRootPath = HostEnvironment.GetWebRootPath();
        var path = Path.Combine(wwwRootPath + folder + fileName);

        if (File.Exists(path)) File.Delete(path);
    }
    
    private static string GetFolderName(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return extension.ToLower() is ".png" or ".jpg" or ".jpeg" or ".gif" or ".tif" ? "/images/" : "/uploads/";
    }
    
    private static string RemoveSpecialCharacters(string str)
    {
        var modifiedStr = new StringBuilder();
        foreach (var character in str.ToLower())
        {
            if (char.IsLetterOrDigit(character)) modifiedStr.Append(character);
        }

        return modifiedStr.ToString();
    }
}