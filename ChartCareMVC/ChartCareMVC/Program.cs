using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChartCareMVC.Data;
using ChartCareMVC.Areas.Identity.Data;
using ChartCareMVC.Configurations;
using Microsoft.AspNetCore.Identity.UI.Services;
using ChartCareMVC.Services;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("CompanyDbContextConnection") ?? throw new InvalidOperationException("Connection string 'CompanyDbContextConnection' not found.");

builder.Services.AddDbContext<CompanyDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<CompanyUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CompanyDbContext>()
    .AddDefaultTokenProviders(); ;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Register SmtpSettings as a singleton service
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Register EmailSender with the DI container
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.SeedRoles(services);
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
public partial class Program { }
