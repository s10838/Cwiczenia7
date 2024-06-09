using Cwiczenia7.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=db-mssql16.pjwstk.edu.pl;Database=s10838;Integrated Security=True;TrustServerCertificate=True"));
     //   .UseLazyLoadingProxies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseAuthorization();

app.MapControllers();

app.Run();


//dotnet ef migrations add InitialCreate
//dotnet ef database update