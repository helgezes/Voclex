using System.Text.Json.Serialization;
using Application.DataAccess;
using Application.ModelInterfaces.DtoInterfaces;
using Application.Models;
using Application.Services;
using Application.Services.Factories.Interfaces;
using Application.Services.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Services.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
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
builder.Services.AddScoped<ICrudService<Term, TermDto>, GenericCrudService<Term, TermDto>>();
builder.Services.AddScoped<ICrudService<TermsDictionary, TermsDictionaryDto>, GenericCrudService<TermsDictionary, TermsDictionaryDto>>();
builder.Services.AddScoped<ICrudService<Definition, DefinitionDto>, GenericCrudService<Definition, DefinitionDto>>();
builder.Services.AddScoped<ICrudService<Example, ExampleDto>, GenericCrudService<Example, ExampleDto>>();
builder.Services.AddScoped<ICrudService<Picture, PictureDto>, GenericCrudService<Picture, PictureDto>>();
builder.Services.AddScoped<IGetListService<Term, TermDto>, GenericGetListService<Term, TermDto>>();
builder.Services.AddScoped<IGetListService<TermsDictionary, TermsDictionaryDto>, GenericGetListService<TermsDictionary, TermsDictionaryDto>>();
builder.Services.AddScoped<ITermRelatedService<DefinitionDto>, TermRelatedService<Definition, DefinitionDto>>();
builder.Services.AddScoped<ITermRelatedService<ExampleDto>, TermRelatedService<Example, ExampleDto>>();
builder.Services.AddScoped<ITermRelatedService<PictureDto>, TermRelatedService<Picture, PictureDto>>();
builder.Services.AddScoped<IFileSavingServiceFactory, DiskFileSavingServiceFactory>();
builder.Services.AddScoped<ICrudService<Picture, IPictureDto>, GenericCrudService<Picture, IPictureDto>>();
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


Directory.CreateDirectory(DiskFileSavingServiceFactory.PicturesFolderPath);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(DiskFileSavingServiceFactory.PicturesFolderPath),
    RequestPath = DiskFileSavingServiceFactory.PicturesEndpointFolderPath.TrimEnd('/')
});

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