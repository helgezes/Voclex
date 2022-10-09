using Application.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

using ApplicationDbContext context = new();

context.Database.EnsureDeleted();
context.Database.Migrate();

Dictionary newDict = new("Oxford 300");
DictionaryItem newDictItem = new("Train", newDict);
Definition newDefinition = new(
    "a connected line of railroad cars with or without a locomotive", newDictItem);

User newUser = new("Joe");
DictionaryItemProgress newProgress = new(newUser, newDictItem);

context.Dictionaries.Add(newDict);
context.DictionaryItems.Add(newDictItem);
context.Definitions.Add(newDefinition);
context.Users.Add(newUser);
context.DictionaryItemProgresses.Add(newProgress);
await context.SaveChangesAsync();

var dicts = context.Dictionaries.ToList();

foreach (var dict in dicts)
{
    Console.WriteLine(dict.Id);   
    Console.WriteLine(dict.Title);
}

Console.Read();