using InkRealmMVC.Models.DbModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
string? connString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING");
builder.Services.AddDbContext<InkRealmContext>(options => 
    {
        options.UseNpgsql(connString);
    });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => 
    { 
        options.LoginPath = "/Verification/Auth";
        options.LogoutPath = "/Verification/Logout";
        options.AccessDeniedPath = "/";
    });

var app = builder.Build();

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
    pattern: "{controller=Home}/{action=Index}");


app.Run();
