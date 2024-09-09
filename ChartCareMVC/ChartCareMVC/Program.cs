using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChartCareMVC.Data;
using ChartCareMVC.Areas.Identity.Data;
using ChartCareMVC.Configurations;
using Microsoft.AspNetCore.Identity.UI.Services;
using ChartCareMVC.Services;
using ChartCareMVC.Services.PricingPlanService;
using ChartCareMVC.Services.FeaturesService;



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

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Register Services
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IPricingPlanService, PricingPlanService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();

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
