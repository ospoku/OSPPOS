using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;

using OSPPOS.Helpers;
using DMX.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using OSPPOS.Data;
using OSPPOS.Services;
using OSPPOS.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? settingsMail = builder.Configuration["Settings:AppEmail"];
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<EntityService>();
builder.Services.AddScoped<AssignmentService>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<ISaleOrderService, SaleOrderService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>(); 
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Specify the login page URL
    options.AccessDeniedPath = "/Views/Shared/AccessDenied"; // Specify the access denied page URL
});


builder.Services.AddSingleton<HttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthorization(options =>
{

});




//builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
//builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

// Add services to the container.
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(10));
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
builder.Services.AddDbContext<XContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("POS")));

builder.Services.AddIdentity<AppUser,AppRole>()
    .AddEntityFrameworkStores<XContext>();
                                                                                                                   

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DBInitializer>();
builder.Services.AddSignalR();
builder.Services.AddDataProtection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseSession();
app.UseNotyf();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy();
//app.MapHub<NotificationHub>("/notificationHub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Splash}/{id?}");

var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<XContext>();
db.Database.EnsureCreatedAsync().Wait();

var init = scope.ServiceProvider.GetRequiredService<DBInitializer>();
await init.Initialize();
app.Run();
