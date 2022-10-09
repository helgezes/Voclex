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
            if (context.Dictionaries.Any()) return;


            Dictionary newDict = new("Test dictionary");
            context.Add(newDict);

            #region DictionaryItems and Definitions

            DictionaryItem newDictItem;
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("view", newDict));
            context.Definitions.Add(new Definition("the things you can see from a place (usually attractive)", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("suburbs", newDict));
            context.Definitions.Add(new Definition("an area where people live outside the centre of a city", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("rough", newDict));
            context.Definitions.Add(new Definition("a ______ area is a place where there is a lot of violence and crime", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("neighbourhood", newDict));
            context.Definitions.Add(new Definition("a part of a town or city where people live", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("basement", newDict));
            context.Definitions.Add(new Definition("a room or area below ground level under a house or building where you can liveor work", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("cellar", newDict));
            context.Definitions.Add(new Definition("a room under a house that is used for storing things", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("en-suite", newDict));
            context.Definitions.Add(new Definition("bathroom a bathroom that is directly connected to a bedroom", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("fitted", newDict));
            context.Definitions.Add(new Definition("kitchen a kitchen where the cupboards, cooker, etc. fit exactly into the space", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("clear", newDict));
            context.Definitions.Add(new Definition("out tidy a room, cupboard, etc. and get rid of the things in it that you don’t want anymore", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("sort", newDict));
            context.Definitions.Add(new Definition("out arrange or organise things that are not in order or are untidy", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("give", newDict));
            context.Definitions.Add(new Definition("away give something to sb without asking for money", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("throw", newDict));
            context.Definitions.Add(new Definition("away put something in the rubbish bin that you don’t want any more", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("tidy", newDict));
            context.Definitions.Add(new Definition("up make a room or place tidy by putting things back in the place where you usuallykeep them", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("put", newDict));
            context.Definitions.Add(new Definition("away put something in the place where you usually keep it", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("go", newDict));
            context.Definitions.Add(new Definition("through carefully look at things to find something or to see if you want to keep them", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("storm", newDict));
            context.Definitions.Add(new Definition("very bad weather with lots of rain, snow, wind, etc.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("thunder", newDict));
            context.Definitions.Add(new Definition("the loud noise that comes from the sky during a storm", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("lightning", newDict));
            context.Definitions.Add(new Definition("a bright light in the sky caused by electricity during a storm, usually followed by thunder", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("gale", newDict));
            context.Definitions.Add(new Definition("a very strong wind", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("shower", newDict));
            context.Definitions.Add(new Definition("a short period of rain", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("hurricane", newDict));
            context.Definitions.Add(new Definition("a violent storm with very strong winds", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("fog", newDict));
            context.Definitions.Add(new Definition("thick cloud just above the ground or sea that makes it difficult to see", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("humid", newDict));
            context.Definitions.Add(new Definition("when the air is hot and wet", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("heat", newDict));
            context.Definitions.Add(new Definition("wave a period of unusually hot weather that continues for a long time", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("pick", newDict));
            context.Definitions.Add(new Definition("choose", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("behave", newDict));
            context.Definitions.Add(new Definition("act in a certain way", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("spot", newDict));
            context.Definitions.Add(new Definition("to notice", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("reveal", newDict));
            context.Definitions.Add(new Definition("show", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("reckon", newDict));
            context.Definitions.Add(new Definition("think, have an opinion", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("reject", newDict));
            context.Definitions.Add(new Definition("not accept", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("sensitive", newDict));
            context.Definitions.Add(new Definition("_______ people are able to understand other people’s feelings and problems and help them in a way that does not upset them.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("brave", newDict));
            context.Definitions.Add(new Definition("_______ people are not frightened in dangerous or difficult situations.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("determined", newDict));
            context.Definitions.Add(new Definition("_______ people want to do something very much and don’t allow anything to stop them.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("reliable", newDict));
            context.Definitions.Add(new Definition("_______ people always do what you want or expect them to do.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("organised", newDict));
            context.Definitions.Add(new Definition("_______ people plan things well and don’t waste time.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("stubborn", newDict));
            context.Definitions.Add(new Definition("_______ people won’t change their ideas/plans when other people want them to.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("ambitious", newDict));
            context.Definitions.Add(new Definition("_______ people want to be very successful or powerful.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("confident", newDict));
            context.Definitions.Add(new Definition("_______ people are sure that they can do things successfully or well.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("generous", newDict));
            context.Definitions.Add(new Definition("_______ people like giving money and presents to other people.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("mean", newDict));
            context.Definitions.Add(new Definition("_______ people don’t like spending money or giving things to other people.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("responsible", newDict));
            context.Definitions.Add(new Definition("_______ people behave sensibly and can make good decisions on their own.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("remarkable", newDict));
            context.Definitions.Add(new Definition("very unusual", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("violent", newDict));
            context.Definitions.Add(new Definition("______ people try to hurt or kill other people.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("arrogant", newDict));
            context.Definitions.Add(new Definition("______ people believe they are better or more important than other people.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("selfish", newDict));
            context.Definitions.Add(new Definition("______ people usually only think about themselves.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("hard-working", newDict));
            context.Definitions.Add(new Definition("______ people work very hard.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("loyal", newDict));
            context.Definitions.Add(new Definition("______ people always support their friends, etc.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("enthusiastic", newDict));
            context.Definitions.Add(new Definition("______ people show a lot of interest and excitement about something.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("considerate", newDict));
            context.Definitions.Add(new Definition("______ people are very kind and helpful.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("spoilt", newDict));
            context.Definitions.Add(new Definition("______ people behave badly because other people always give them what they want or allow them to do what they want (often used for children).", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("offensive", newDict));
            context.Definitions.Add(new Definition("______ people often upset or embarrass people by the things they say or how they behave.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("helpful", newDict));
            context.Definitions.Add(new Definition("______ people like helping other people.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("moody", newDict));
            context.Definitions.Add(new Definition("______ people are often unfriendly because they are angry and unhappy.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("bad-tempered", newDict));
            context.Definitions.Add(new Definition("______ people become annoyed or angry easily.", newDictItem));
            context.DictionaryItems.Add(newDictItem = new DictionaryItem("well-behaved", newDict));
            context.Definitions.Add(new Definition("______ people behave in a quiet and polite way", newDictItem));


            #endregion

            context.Users.Add(new User("Joe"));

            await context.SaveChangesAsync();
        }
    }
}
