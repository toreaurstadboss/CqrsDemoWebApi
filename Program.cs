using CqrsDemoWebApi.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly()); //adding MediatR support here. 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = new ConnectionString(builder.Configuration.GetConnectionString("CqrsDemoBooksDb")); 
builder.Services.AddSingleton(connectionString);

builder.Services.AddDbContext<BooksDbContext>(options =>
options.UseSqlServer(connectionString.Value)); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
