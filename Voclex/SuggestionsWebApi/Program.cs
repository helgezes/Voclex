using Microsoft.AspNetCore.Mvc;
using SuggestionsWebApi.Services;
using SuggestionsWebApi.Services.Implementations;
using SuggestionsWebApi.Services.Implementations.Interfaces;
using SuggestionsWebApi.Services.Interfaces;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options => options.AddDefaultPolicy(c => c.WithOrigins("http://localhost:5181", "https://localhost:7181").AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDefinitionSuggestionsService, MockDefinitionService>();
builder.Services.AddScoped<IDefinitionsService, DefinitionsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
