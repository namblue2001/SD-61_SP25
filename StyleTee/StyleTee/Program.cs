using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StyleTee.Data;
using StyleTee.Models;
using StyleTee.Services;

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

builder.Services.AddHttpClient<GHNService>(client =>
{
    client.BaseAddress = new Uri("https://online-gateway.ghn.vn");
    client.DefaultRequestHeaders.Add("Token", "12f949a2-0c6e-11f0-bdfc-6a0eda42fc14");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.32.3");
    client.DefaultRequestHeaders.Add("Host", "online-gateway.ghn.vn");
    client.DefaultRequestHeaders.Add("Origin", "https://online-gateway.ghn.vn");
    client.DefaultRequestHeaders.Add("Referer", "https://online-gateway.ghn.vn/");
});

builder.Services.AddScoped<GHNService>();

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

