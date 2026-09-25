using APL.Models;
using APL.Interfaces;
using APL.Services;
using BLL.Interfaces;
using BLL.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme =
//    x.DefaultChallengeScheme =
//    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(y =>
//{
//    y.SaveToken = false;
//    y.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(
//                    builder.Configuration["AppSettings:JWTSecret"]!))
//    };
//});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
