using Application.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using ApplicationDbContext context = new();

context.Database.EnsureDeleted();
context.Database.Migrate();

context.Dictionaries.Add(new Dictionary("Oxford 300"));
await context.SaveChangesAsync();

var dicts = context.Dictionaries.ToList();

foreach (var dict in dicts)
{
    Console.WriteLine(dict.Id);   
    Console.WriteLine(dict.Title);
}

Console.Read();