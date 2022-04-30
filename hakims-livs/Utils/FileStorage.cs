namespace hakims_livs.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class HostEnvironment{
    public static IWebHostEnvironment? GetWebHostEnvironment()
    {
        var _accessor = new HttpContextAccessor();
        return _accessor.HttpContext?.RequestServices.GetRequiredService<IWebHostEnvironment>();
    }
}
public static class FileStorage
{
    public static async Task<string> Store(IFormFile file, string name)
    {

        string fileName;
        try
        {
            var wwwRootPath = HostEnvironment.GetWebHostEnvironment()?.WebRootPath;
            var extension = Path.GetExtension(file.FileName);
            fileName = DateTime.Now.ToString("yymmssfff") + "_" + name + extension;

            var folder = file.ContentType.Contains("image") ? "/images/" : "/uploads/";
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
}