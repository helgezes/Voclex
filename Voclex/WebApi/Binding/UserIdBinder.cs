using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedLibrary.Services.Interfaces;

namespace WebApi.Binding;

public sealed class UserIdBinder : IModelBinder
{
    private readonly IAuthenticatedUserService authenticatedUserService;

    public UserIdBinder(IAuthenticatedUserService authenticatedUserService)
    {
        this.authenticatedUserService = authenticatedUserService;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var user = await authenticatedUserService.GetCurrentUser();
        if (user == null)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }

        bindingContext.Result = ModelBindingResult.Success(user.Id);
    }
}