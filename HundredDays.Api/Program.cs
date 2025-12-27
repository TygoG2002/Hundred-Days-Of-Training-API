using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorDev", policy =>
    {
        policy
            .WithOrigins("https://localhost:7288")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();


builder.Services.AddApplication();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddInfrastructure(connectionString!);

var app = builder.Build();
app.UseCors("BlazorDev");

app.UseAuthorization();
app.MapControllers();

app.Run();
