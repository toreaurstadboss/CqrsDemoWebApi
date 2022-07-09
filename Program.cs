using CqrsDemoWebApi.Database;
using CqrsDemoWebApi.Features.Books.Pipeline;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
 lc.WriteTo.Console()
 .WriteTo.Seq("http://localhost:5341")); //note - to use Seq with this url - install it from here (free individual license) : https://datalust.co/download

// Add services to the container.

builder.Services.AddMediatR(Assembly.GetExecutingAssembly()); //adding MediatR support here. 
//register some pipeline(s) defined for MediatR usage
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); 

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
