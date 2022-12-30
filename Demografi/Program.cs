using Demografi.Services;
using Demografi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAccountService, AccountServiceImpl>();

builder.Services.AddDbContext<DataContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.EnsureCreated();
    DataDummy.Initialize(context);

    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError("An error occurred creating the DB.");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=LoginController}/{action=Index}/{id?}");

//app.MapControllerRoute(name: "Login",
//                pattern: "login/{*index}",
//                defaults: new { controller = "Login", action = "Index" });

app.MapControllerRoute(name: "Home",
                pattern: "Home/{*index}",
                defaults: new { controller = "Home", action = "Index" });

ControllerActionEndpointConventionBuilder controllerActionEndpointConventionBuilder = app.MapControllerRoute(name: "default",
               pattern: "{controller=Login}/{action=Index}/{id?}");


app.Run();
