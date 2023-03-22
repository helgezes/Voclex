using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using SharedLibrary.Attributes;

namespace WebApi.Binding
{
    public sealed class UserIdBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (IsUserIdAttributeApplied(context))
            {
                return new BinderTypeModelBinder(typeof(UserIdBinder));
            }

            return null;
        }

        private static bool IsUserIdAttributeApplied(ModelBinderProviderContext context)
        {
            return context.Metadata is DefaultModelMetadata metadata && metadata.Attributes.Attributes.Any(a => a.GetType() == typeof(UserIdAttribute));
        }
    }
}
