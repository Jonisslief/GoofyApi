using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

var app  = builder.Build();

app.Run();