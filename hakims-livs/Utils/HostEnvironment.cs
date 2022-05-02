namespace hakims_livs.Utils;
using Microsoft.Extensions.DependencyInjection;

public static class HostEnvironment{
    public static string? GetWebRootPath()
    {
        var accessor = new HttpContextAccessor();
        var webHostEnvironment = accessor.HttpContext?.RequestServices.GetRequiredService<IWebHostEnvironment>();
        return webHostEnvironment?.WebRootPath;
    }
}