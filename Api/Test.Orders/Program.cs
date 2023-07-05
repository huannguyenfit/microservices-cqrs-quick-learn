using Microsoft.EntityFrameworkCore;

using AutoMapper;
using System.Reflection;
using Test.Core.Extension;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("MainConnection");
builder.Services.AddCoreExtension(connectionString);
builder.Services.Configure<ApiBehaviorOptions>((option) => option.SuppressModelStateInvalidFilter = true);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
