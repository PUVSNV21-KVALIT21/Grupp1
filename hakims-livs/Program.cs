using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using hakims_livs.Data;
using hakims_livs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using hakims_livs.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cloudConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTIONSTRING");

if (cloudConnectionString != null)
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(cloudConnectionString));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<Customer>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddScoped<ICustomer, CustomerData>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<AccessControl>();
// Added service for sending confirmation email
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    var env = services.GetRequiredService<IWebHostEnvironment>();
    var usermanager = services.GetRequiredService<UserManager<Customer>>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context, env);
    await DbInitializer.CreateUser(context, usermanager, 10);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

