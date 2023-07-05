using Application.DataAccess;
using Application.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using SharedLibrary.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Enums;
using SharedLibrary.ModelInterfaces.DtoInterfaces;

namespace Infrastructure.Filters
{
    public sealed class DictionaryIdValidationActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var model in context.ActionArguments.Values)
            {
                if (model == null) continue;
                
                var validationResult = await CheckUserAccessToDictionary(model, context.HttpContext.RequestServices);

                if (validationResult == ValidationResult.Success) continue;

                context.Result = new BadRequestObjectResult(new { message = validationResult!.ErrorMessage });
                return;
            }

            await next();
        }

        private async Task<ValidationResult?> CheckUserAccessToDictionary(object model, IServiceProvider serviceProvider)
        {
            var modelType = model.GetType();
            var propertiesWithAttribute = GetPropertiesWithDictionaryIdValidationAttribute(modelType);

            foreach (var property in propertiesWithAttribute)
            {
                var value = property.GetValue(model) as int?;

                if(value == null) return new ValidationResult("The dictionary id should be integer");
                
                var user = await GetCurrentUser(serviceProvider);

                if (user == null)
                {
                    return new ValidationResult("User must be authenticated");
                }

                var userId = user.Role == Role.Admin ? (int?)null : user.Id;

                var dbContext = serviceProvider.GetRequiredService<IDbContext>();
                if (!(await dbContext.TermsDictionaries.AnyAsync(d => d.Id == value && d.UserId == userId)))
                {
                    return new ValidationResult("You cannot update this dictionary");
                }
            }

            return ValidationResult.Success;
        }

        private static async Task<IUserDto?> GetCurrentUser(IServiceProvider serviceProvider)
        {
            var userService = serviceProvider.GetRequiredService<IAuthenticatedUserService>();

            var user = await userService.GetCurrentUser();
            return user;
        }

        private static IEnumerable<PropertyInfo> GetPropertiesWithDictionaryIdValidationAttribute(Type modelType)
        {
            var propertiesWithAttribute = modelType.GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(DictionaryIdValidationAttribute)));
            return propertiesWithAttribute;
        }
    }

}
