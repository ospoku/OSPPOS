using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using DMX.Authorization;
using OSPPOS.Helpers;
using DMX.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using OSPPOS.Models;
using OSPPOS.Data;
using OSPPOS.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string? settingsMail = builder.Configuration["Settings:AppEmail"];
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<SMSService>();
builder.Services.AddScoped<FeeService>();
builder.Services.AddScoped<EntityService>();
builder.Services.AddScoped<AssignmentService>();
builder.Services.AddScoped<AllowanceService>();

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
    options.AddPolicy("TravelRequestOwnerPolicy", policy =>
        policy.Requirements.Add(new TravelRequestOwnerRequirement()));

    options.AddPolicy("MemoOwnerPolicy", policy =>
        policy.Requirements.Add(new MemoOwnerRequirement()));

    options.AddPolicy("ExcuseDutyOwnerPolicy", policy =>
        policy.Requirements.Add(new ExcuseDutyOwnerRequirement()));
    options.AddPolicy("DeceasedOwnerPolicy", policy =>
        policy.Requirements.Add(new DeceasedOwnerRequirement()));
    
});

// Register handlers
builder.Services.AddSingleton<IAuthorizationHandler, TravelRequestOwnerHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MemoOwnerHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ExcuseDutyOwnerHandler>();


builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

// Add services to the container.
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(10));
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
builder.Services.AddDbContext<XContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DMX")));

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

//var scope = app.Services.CreateScope();
//var db = scope.ServiceProvider.GetRequiredService<XContext>();
//db.Database.EnsureCreatedAsync().Wait();

//var init = scope.ServiceProvider.GetRequiredService<DBInitializer>();
//await init.Initialize();
app.Run();
