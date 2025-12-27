var builder = WebApplication.CreateBuilder(args);

// ?? CORS policy
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

var app = builder.Build();

// ? CORS MOET vóór MapControllers
app.UseCors("BlazorDev");

app.UseAuthorization();
app.MapControllers();

app.Run();
