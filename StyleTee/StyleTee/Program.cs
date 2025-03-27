using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Kích hoạt Session
builder.Services.AddDistributedMemoryCache(); // Cần thiết để Session hoạt động
builder.Services.AddSingleton<IConfiguration>(builder.Configuration); // Quên mk


// Add session configuration
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseSession(); // Kích hoạt Session Middleware

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Add session middleware
app.UseSession();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Access}/{action=DangNhap}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=DangNhap}/{id?}");
app.Run();

