global using OnlineStore.Services.ProductService;
global using OnlineStore.Services.OrderService;
global using OnlineStore.Services.LogInService;
global using OnlineStore.Services.ProductTypeService;
global using OnlineStore.Models;
global using OnlineStore.DTOs.Product;
global using OnlineStore.DTOs.Order;
global using OnlineStore.DTOs.User;
global using OnlineStore.DTOs.ProductType;
global using OnlineStore.DTOs.OrderProduct;
global using OnlineStore.Data;
global using OnlineStore;
global using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using OnlineStore.Controllers;
using Microsoft.AspNetCore.Builder;
using OnlineStore.Services.OrderProduct;
using OnlineStore.Data;
using OnlineStore.Services.LogInService;
using OnlineStore.Services.OrderProduct;
using OnlineStore.Services.OrderService;
using OnlineStore.Services.ProductService;
using OnlineStore.Services.ProductTypeService;
using OnlineStore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(); // app.MapControllers();????????????????????????????????
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ILogInService, LogInService>();
builder.Services.AddTransient<IProductTypeService, ProductTypeService>();
builder.Services.AddTransient<IOrderProductService, OrderProductService>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(); //!

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(
    options =>
    {
        options.Cookie.Name = "Cookie";
        options.LoginPath = "/LogIn";
        options.AccessDeniedPath = "/LogIn/AccessDenied";
        //options.Cookie.HttpOnly = false;
        //options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        options.Cookie.SameSite = SameSiteMode.None;
    });

//var cookieOpt = new CookieOptions()
//{
//    Path = "/",
//    Expires = DateTimeOffset.UtcNow.AddDays(1),
//    IsEssential = true,
//    HttpOnly = false,
//    Secure = false,
//};

builder.Services.AddAuthorization();

var app = builder.Build();

//app.UseCors(builder => builder.AllowAnyOrigin());
app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
//app.UseCookiePolicy(new CookieOptions
//{
//    HttpOnly = true,
//    Secure = false,//context.Request.IsHttps,
//    Expires = DateTimeOffset.Now.AddDays(1)
//});

app.MapControllers();

using var scope = app.Services.CreateScope();
DataContext context = scope.ServiceProvider.GetRequiredService<DataContext>();
await DbInitializer.Initialize(context);

app.Run();
