using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextInitializer
    {
        public static async Task CreateAndSeedDbIfNeeded(ApplicationDbContext context)
        {
            //await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();
            await SeedDbIfNeeded(context);
        }

        public static async Task SeedDbIfNeeded(ApplicationDbContext context)
        {
            if (context.TermsDictionaries.Any()) return;


            TermsDictionary newDict = new("Test dictionary");
            context.Add(newDict);

            for (var i = 2; i < 20; i++)
            {
                context.Add(new TermsDictionary("Test dictionary " + i.ToString()));
            }

            #region Terms, Definitions, Pictures and Examples

            Term newDictItem;
            context.Terms.Add(newDictItem = new Term("view", newDict));
            context.Definitions.Add(new Definition("the things you can see from a place (usually attractive)", newDictItem));
            context.Examples.Add(new Example(@"•There were magnificent views of the surrounding countryside.
•Most rooms enjoy panoramic views of the ocean.
•The view from the top of the tower was spectacular.
•I'd like a room with a view.", newDictItem));
            context.Pictures.Add(new Picture("/images/alps-g582e2e84e_640.jpg", newDictItem));
            context.Terms.Add(newDictItem = new Term("suburbs", newDict));
            context.Definitions.Add(new Definition("an area where people live outside the centre of a city", newDictItem)); 
            context.Examples.Add(new Example(@"•a London suburb
•They live in the suburbs.", newDictItem));
            context.Pictures.Add(new Picture("/images/suburbs-g947d6d4e3_640.jpg", newDictItem));
            context.Terms.Add(newDictItem = new Term("rough", newDict));
            context.Definitions.Add(new Definition("a ______ area is a place where there is a lot of violence and crime", newDictItem));
            context.Terms.Add(newDictItem = new Term("neighbourhood", newDict));
            context.Definitions.Add(new Definition("a part of a town or city where people live", newDictItem));
            context.Pictures.Add(new Picture("/images/street-g3c18705ab_640.jpg", newDictItem));
            context.Terms.Add(newDictItem = new Term("basement", newDict)); 
            context.Examples.Add(new Example(@"•Kitchen goods are sold in the basement.", newDictItem));
            context.Definitions.Add(new Definition("a room or area below ground level under a house or building where you can liveor work", newDictItem));
            context.Terms.Add(newDictItem = new Term("cellar", newDict));
            context.Examples.Add(new Example(@"•We looked all over the house, even down in the coal cellar.
•We keep onions and apples in the cellar.", newDictItem));
            context.Definitions.Add(new Definition("a room under a house that is used for storing things", newDictItem));
            context.Pictures.Add(new Picture("/images/cellar.gif", newDictItem));
            context.Terms.Add(newDictItem = new Term("en-suite", newDict));
            context.Definitions.Add(new Definition("bathroom a bathroom that is directly connected to a bedroom", newDictItem));
            context.Terms.Add(newDictItem = new Term("fitted", newDict));
            context.Definitions.Add(new Definition("kitchen a kitchen where the cupboards, cooker, etc. fit exactly into the space", newDictItem));
            context.Terms.Add(newDictItem = new Term("clear", newDict));
            context.Definitions.Add(new Definition("out tidy a room, cupboard, etc. and get rid of the things in it that you don’t want anymore", newDictItem));
            context.Terms.Add(newDictItem = new Term("sort", newDict));
            context.Definitions.Add(new Definition("out arrange or organise things that are not in order or are untidy", newDictItem));
            context.Terms.Add(newDictItem = new Term("give", newDict));
            context.Definitions.Add(new Definition("away give something to sb without asking for money", newDictItem));
            context.Terms.Add(newDictItem = new Term("throw", newDict));
            context.Definitions.Add(new Definition("away put something in the rubbish bin that you don’t want any more", newDictItem));
            context.Terms.Add(newDictItem = new Term("tidy", newDict));
            context.Definitions.Add(new Definition("up make a room or place tidy by putting things back in the place where you usuallykeep them", newDictItem));
            context.Terms.Add(newDictItem = new Term("put", newDict));
            context.Definitions.Add(new Definition("away put something in the place where you usually keep it", newDictItem));
            context.Terms.Add(newDictItem = new Term("go", newDict));
            context.Definitions.Add(new Definition("through carefully look at things to find something or to see if you want to keep them", newDictItem));
            context.Terms.Add(newDictItem = new Term("storm", newDict));
            context.Definitions.Add(new Definition("very bad weather with lots of rain, snow, wind, etc.", newDictItem));
            context.Terms.Add(newDictItem = new Term("thunder", newDict));
            context.Definitions.Add(new Definition("the loud noise that comes from the sky during a storm", newDictItem));
            context.Terms.Add(newDictItem = new Term("lightning", newDict));
            context.Definitions.Add(new Definition("a bright light in the sky caused by electricity during a storm, usually followed by thunder", newDictItem));
            context.Terms.Add(newDictItem = new Term("gale", newDict));
            context.Definitions.Add(new Definition("a very strong wind", newDictItem));
            context.Terms.Add(newDictItem = new Term("shower", newDict));
            context.Definitions.Add(new Definition("a short period of rain", newDictItem));
            context.Terms.Add(newDictItem = new Term("hurricane", newDict));
            context.Definitions.Add(new Definition("a violent storm with very strong winds", newDictItem));
            context.Terms.Add(newDictItem = new Term("fog", newDict));
            context.Definitions.Add(new Definition("thick cloud just above the ground or sea that makes it difficult to see", newDictItem));
            context.Terms.Add(newDictItem = new Term("humid", newDict));
            context.Definitions.Add(new Definition("when the air is hot and wet", newDictItem));
            context.Terms.Add(newDictItem = new Term("heat", newDict));
            context.Definitions.Add(new Definition("wave a period of unusually hot weather that continues for a long time", newDictItem));
            context.Terms.Add(newDictItem = new Term("pick", newDict));
            context.Definitions.Add(new Definition("choose", newDictItem));
            context.Terms.Add(newDictItem = new Term("behave", newDict));
            context.Definitions.Add(new Definition("act in a certain way", newDictItem));
            context.Terms.Add(newDictItem = new Term("spot", newDict));
            context.Definitions.Add(new Definition("to notice", newDictItem));
            context.Terms.Add(newDictItem = new Term("reveal", newDict));
            context.Definitions.Add(new Definition("show", newDictItem));
            context.Terms.Add(newDictItem = new Term("reckon", newDict));
            context.Definitions.Add(new Definition("think, have an opinion", newDictItem));
            context.Terms.Add(newDictItem = new Term("reject", newDict));
            context.Definitions.Add(new Definition("not accept", newDictItem));
            context.Terms.Add(newDictItem = new Term("sensitive", newDict));
            context.Definitions.Add(new Definition("_______ people are able to understand other people’s feelings and problems and help them in a way that does not upset them.", newDictItem));
            context.Terms.Add(newDictItem = new Term("brave", newDict));
            context.Definitions.Add(new Definition("_______ people are not frightened in dangerous or difficult situations.", newDictItem));
            context.Terms.Add(newDictItem = new Term("determined", newDict));
            context.Definitions.Add(new Definition("_______ people want to do something very much and don’t allow anything to stop them.", newDictItem));
            context.Terms.Add(newDictItem = new Term("reliable", newDict));
            context.Definitions.Add(new Definition("_______ people always do what you want or expect them to do.", newDictItem));
            context.Terms.Add(newDictItem = new Term("organised", newDict));
            context.Definitions.Add(new Definition("_______ people plan things well and don’t waste time.", newDictItem));
            context.Terms.Add(newDictItem = new Term("stubborn", newDict));
            context.Definitions.Add(new Definition("_______ people won’t change their ideas/plans when other people want them to.", newDictItem));
            context.Terms.Add(newDictItem = new Term("ambitious", newDict));
            context.Definitions.Add(new Definition("_______ people want to be very successful or powerful.", newDictItem));
            context.Terms.Add(newDictItem = new Term("confident", newDict));
            context.Definitions.Add(new Definition("_______ people are sure that they can do things successfully or well.", newDictItem));
            context.Terms.Add(newDictItem = new Term("generous", newDict));
            context.Definitions.Add(new Definition("_______ people like giving money and presents to other people.", newDictItem));
            context.Terms.Add(newDictItem = new Term("mean", newDict));
            context.Definitions.Add(new Definition("_______ people don’t like spending money or giving things to other people.", newDictItem));
            context.Terms.Add(newDictItem = new Term("responsible", newDict));
            context.Definitions.Add(new Definition("_______ people behave sensibly and can make good decisions on their own.", newDictItem));
            context.Terms.Add(newDictItem = new Term("remarkable", newDict));
            context.Definitions.Add(new Definition("very unusual", newDictItem));
            context.Terms.Add(newDictItem = new Term("violent", newDict));
            context.Definitions.Add(new Definition("______ people try to hurt or kill other people.", newDictItem));
            context.Terms.Add(newDictItem = new Term("arrogant", newDict));
            context.Definitions.Add(new Definition("______ people believe they are better or more important than other people.", newDictItem));
            context.Terms.Add(newDictItem = new Term("selfish", newDict));
            context.Definitions.Add(new Definition("______ people usually only think about themselves.", newDictItem));
            context.Terms.Add(newDictItem = new Term("hard-working", newDict));
            context.Definitions.Add(new Definition("______ people work very hard.", newDictItem));
            context.Terms.Add(newDictItem = new Term("loyal", newDict));
            context.Definitions.Add(new Definition("______ people always support their friends, etc.", newDictItem));
            context.Terms.Add(newDictItem = new Term("enthusiastic", newDict));
            context.Definitions.Add(new Definition("______ people show a lot of interest and excitement about something.", newDictItem));
            context.Terms.Add(newDictItem = new Term("considerate", newDict));
            context.Definitions.Add(new Definition("______ people are very kind and helpful.", newDictItem));
            context.Terms.Add(newDictItem = new Term("spoilt", newDict));
            context.Definitions.Add(new Definition("______ people behave badly because other people always give them what they want or allow them to do what they want (often used for children).", newDictItem));
            context.Terms.Add(newDictItem = new Term("offensive", newDict));
            context.Definitions.Add(new Definition("______ people often upset or embarrass people by the things they say or how they behave.", newDictItem));
            context.Terms.Add(newDictItem = new Term("helpful", newDict));
            context.Definitions.Add(new Definition("______ people like helping other people.", newDictItem));
            context.Terms.Add(newDictItem = new Term("moody", newDict));
            context.Definitions.Add(new Definition("______ people are often unfriendly because they are angry and unhappy.", newDictItem));
            context.Terms.Add(newDictItem = new Term("bad-tempered", newDict));
            context.Definitions.Add(new Definition("______ people become annoyed or angry easily.", newDictItem));
            context.Terms.Add(newDictItem = new Term("well-behaved", newDict));
            context.Definitions.Add(new Definition("______ people behave in a quiet and polite way", newDictItem));


            #endregion

            context.Users.Add(new User("Joe"));

            await context.SaveChangesAsync();
        }
    }
}
