using APL.Interfaces;
using APL.Models;
using APL.Services;
using BLL.Interfaces;
using BLL.Managers;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Fill in the JWT token",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var serverVersion = new MariaDbServerVersion(new Version(10, 11, 14));
builder.Services.AddDbContext<DataContext>(x => x.UseMySql("userid=u2_Hc6tMrJeZK;password=tWLf@WyBqo!BUHbOaR8n720u;host=212.227.171.102;database=s2_GoofyApi;persist security info=True", serverVersion));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IBenchService, BenchService>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IBenchManager, BenchManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBenchRepository, BenchRepository>();

builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = 
            x.DefaultChallengeScheme = 
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(y =>
        {
            y.SaveToken = false;
            y.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["AppSettings:JWTSecret"]!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            y.Audience = "goofyapi";
        });
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
