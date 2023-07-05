using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using SharedLibrary.Attributes;
using SharedLibrary.ModelInterfaces.DtoInterfaces;
using SharedLibrary.Services.Interfaces;

namespace WebApi.Binding;

public class UserIdInputFormatter : SystemTextJsonInputFormatter
{
    public UserIdInputFormatter(JsonOptions options, ILogger<SystemTextJsonInputFormatter> logger)
        : base(options, logger)
    {
    }

    public override bool CanRead(InputFormatterContext context)
    {
        return GetPropertiesWithUserIdAttribute(context.Metadata.ModelType).Any();
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
    {
        var result = await base.ReadRequestBodyAsync(context);

        if (result.HasError) return result;
        
        var propertiesWithUserIdAttribute = GetPropertiesWithUserIdAttribute(context.ModelType);

        var currentUser = await GetCurrentUser(context);

        if (currentUser == null) return await InputFormatterResult.FailureAsync();

        SetValueOfPropertiesToUserId(propertiesWithUserIdAttribute, result.Model, currentUser);

        return result;
    }

    private static void SetValueOfPropertiesToUserId(
        IEnumerable<PropertyInfo> propertiesWithUserIdAttribute,
        object? modelInstance,
        IUserDto currentUser)
    {
        foreach (var property in propertiesWithUserIdAttribute)
        {
            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                property.SetValue(modelInstance, currentUser.Id);
            }
        }
    }

    private static async Task<IUserDto?> GetCurrentUser(InputFormatterContext context)
    {
        var authenticatedUserService = context.HttpContext.RequestServices.GetRequiredService<IAuthenticatedUserService>();
        var currentUser = await authenticatedUserService.GetCurrentUser();
        return currentUser;
    }

    private static IEnumerable<PropertyInfo> GetPropertiesWithUserIdAttribute(Type type)
    {
        return type.GetProperties()
            .Where(p => p.GetCustomAttributes(typeof(UserIdAttribute), true).Length > 0);
    }
}