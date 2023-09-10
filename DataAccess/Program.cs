using DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer("Data Source=.; Initial Catalog=NewGalaxyClinic; Integrated Security=true; TrustServerCertificate=true");
});

app.MapGet("/", () => "Hello World!");

app.Run();
