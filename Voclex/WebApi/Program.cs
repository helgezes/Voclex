using Application.DataAccess;
using Application.Models;
using Application.Services;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Shared;

[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IDbContext>(provider => 
    provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddAutoMapper(typeof(ApplicationDbContext));

builder.Services.AddScoped<TermProgressService>();
builder.Services.AddScoped<GenericCrudService<Term, TermDto>>();

var app = builder.Build();

await SeedDevelopmentDb(app);

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


Task SeedDevelopmentDb(WebApplication app)
{
    if (!app.Environment.IsDevelopment()) return Task.CompletedTask;

    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    return ApplicationDbContextInitializer.CreateAndSeedDbIfNeeded(context);
}