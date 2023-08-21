using haymatlosApi.Models;
using haymatlosApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PostgresContext>(rdbs => rdbs.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDatabase")));
builder.Services.AddScoped<UserService>();

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


//need elastic search
//maybe add redis or kafka and stuff
//definitely docker the backend
//learn how to paginate
//also try to improve endpoints, for example use GET on 1 million users.