using AcmeCorp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using AcmeCorp.Repository;
using AcmeCorp.Repository.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IBlogsRepository, BlogsRepository>();


builder.Services.AddControllersWithViews(config =>
{
})
.AddRazorRuntimeCompilation()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//this should be last
app.UseEndpoints(endpoints =>
{
    //area router, should handle all the default routing for areas.
    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute("Login", "Login", new { Controller = "Account", Action = "Login" });
    endpoints.MapControllerRoute("LogOff", "LogOff", new { Controller = "Account", Action = "LogOff" });
    endpoints.MapControllerRoute("Register", "Register", new { Controller = "Account", Action = "Register" });

    endpoints.MapControllerRoute(
       "Default",
      "{controller=Home}/{action=Index}/{id?}"
    );
});

SampleData.InitializeSeedDataWithUserManager(app.Services);

app.Run();
