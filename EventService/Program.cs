using Microsoft.OpenApi.Models;
using SpaceEventService;

var builder = WebApplication.CreateBuilder(args);

// Строка регистрации сервиса

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{    
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(c =>
    {
       c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

