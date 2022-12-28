using System.Text.Json.Serialization;
using Application.DataAccess;
using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using Application.Services;
using Application.Services.Factories;
using Application.Services.Factories.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.DataTransferObjects;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(c => c.WithOrigins("http://localhost:5181", "https://localhost:7181").AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IDbContext>(provider => 
    provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddAutoMapper(typeof(ApplicationDbContext));

builder.Services.AddScoped<TermProgressService>();
builder.Services.AddScoped<GenericCrudService<Term, TermDto>>();
builder.Services.AddScoped<GenericCrudService<TermsDictionary, TermsDictionaryDto>>();
builder.Services.AddScoped<GenericCrudService<Definition, DefinitionDto>>();
builder.Services.AddScoped<GenericCrudService<Example, ExampleDto>>();
builder.Services.AddScoped<GenericCrudService<Picture, PictureDto>>();
builder.Services.AddScoped<GenericGetListService<Term, TermDto>>();
builder.Services.AddScoped<GenericGetListService<TermsDictionary, TermsDictionaryDto>>();
builder.Services.AddScoped<TermRelatedService<Definition, DefinitionDto>>();
builder.Services.AddScoped<TermRelatedService<Example, ExampleDto>>();
builder.Services.AddScoped<TermRelatedService<Picture, PictureDto>>();
builder.Services.AddScoped<IFileSavingServiceFactory, DiskFileSavingServiceFactory>();
builder.Services.AddScoped<GenericCrudService<Picture, IPictureDto>>();
builder.Services.AddScoped<PicturesService>();

var app = builder.Build();

await SeedDevelopmentDb(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseStaticFiles();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


Task SeedDevelopmentDb(WebApplication app)
{
    if (!app.Environment.IsDevelopment()) return Task.CompletedTask;

    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    return ApplicationDbContextInitializer.CreateAndSeedDbIfNeeded(context);
}