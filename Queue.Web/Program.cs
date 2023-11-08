using Microsoft.EntityFrameworkCore;
using Queue.Web.Models;
using Queue.Web.Transacctions;
using Queue.Web.Transacctions.Invoker;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllersWithViews();

// Servicio Command
builder.Services.AddScoped<ClientAdvisor>();

var app = builder.Build();

// Iniciar Servicio Command
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    try
    {
        var queueService = services.GetRequiredService<ClientAdvisor>();
        queueService.ChargeCustomers.Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
