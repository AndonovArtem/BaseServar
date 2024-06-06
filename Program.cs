using Microsoft.EntityFrameworkCore;
using Repository;


DotNetEnv.Env.Load();
var dotenv = Environment.GetEnvironmentVariables();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseNpgsql($"""
                    Host={dotenv["DB_HOST"]};
                    Port={dotenv["DB_PORT"]};
                    Database={dotenv["DB_DATABASE"]};
                    Username={dotenv["DB_USER"]};
                    Password={dotenv["DB_PASSWORD"]}
                    """));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHostedService<TodoController>();
builder.Services.AddScoped<IRepository, PostgresRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();


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
