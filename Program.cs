using SirmaWebApp.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<EmployeeService>();

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}"
    );

app.MapControllerRoute(
    name: "demo",
    pattern: "{controller}/{action}",
    defaults: new { controller = "Demo", action = "Add" }
    );

app.Run();
