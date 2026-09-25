using BLL.Interfaces;
using BLL.Managers;
using DAL.Interfaces;
using DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
