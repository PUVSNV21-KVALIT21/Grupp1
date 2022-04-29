using Microsoft.EntityFrameworkCore;
using hakims_livs.Data;
using hakims_livs.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlite(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();



var cloudConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTIONSTRING");
var localConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(localConnectionString));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(cloudConnectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    
}






builder.Services.AddDefaultIdentity<Customer>(options => options.SignIn.RequireConfirmedAccount = true)
    //.AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AccessControl>();

var app = builder.Build();

using (var scope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    scope?.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

