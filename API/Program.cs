using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mingle Api",
        Version = "v1",
        Description = "Mingle Api is a dating software application.",
        Contact = new OpenApiContact
        {
            Name = "Ugochukwu Umerie",
            Email = "umerieugochukwu@gmail.com"
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/swagger/v1/swagger.json",
         "Mingle Api Documentation v1"));
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
