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
builder.Services.AddInfrastructure();
builder.Services.AddApplication(); 


var app = builder.Build();
app.UseCors("BlazorDev");

app.UseAuthorization();
app.MapControllers();

app.Run();
