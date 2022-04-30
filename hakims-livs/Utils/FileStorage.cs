namespace hakims_livs.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

static class HostEnvironment{
    public static string? GetWebRootPath()
    {
        var _accessor = new HttpContextAccessor();
        var webHostEnvironment = _accessor.HttpContext?.RequestServices.GetRequiredService<IWebHostEnvironment>();
        return webHostEnvironment?.WebRootPath;
    }
}
public static class FileStorage
{
    public static async Task<string> Store(IFormFile file, string name)
    {

        string fileName;
        try
        {
            var wwwRootPath = HostEnvironment.GetWebRootPath();
            var extension = Path.GetExtension(file.FileName);
            fileName = DateTime.Now.ToString("yymmssfff") + "_" + name + extension;

            var folder = GetFolder(extension);
            var path = Path.Combine(wwwRootPath + folder, fileName);
            await using var fileStream = new FileStream(path, FileMode.Create); 
            await file.CopyToAsync(fileStream);
        }
        catch
        {
            throw new Exception("Error trying to store the file");
        }

        
        return fileName;
    }

    private static string GetFolder(string extension)
    {
        extension = extension.ToLower();
        var folder = extension is ".png" or ".jpg" or ".jpeg" or ".gif" or ".tif" ? "/images/" : "/uploads/";
        return folder;
    }

    public static void Delete(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        var folder = GetFolder(extension);
        var wwwRootPath = HostEnvironment.GetWebRootPath();
        var path = Path.Combine(wwwRootPath + folder + fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}