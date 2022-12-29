using Demografi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAccountService, AccountServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=LoginController}/{action=Index}/{id?}");

app.MapControllerRoute(name: "Login",
                pattern: "login/{*index}",
                defaults: new { controller = "Login", action = "Index" });

app.MapControllerRoute(name: "Home",
                pattern: "home/{*index}",
                defaults: new { controller = "Home", action = "Index" });

app.Run();
