using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.OpenApi.Models;
using SharedLibrary.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using SharedLibrary.DataTransferObjects;

namespace WebApi.Filters
{
    public sealed class PropertyHidingFilter : IOperationFilter
    {
        private static readonly Type HidingAttributeType = typeof(UserIdAttribute);

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parametersToHide = GetParametersToHide(context);

            if (parametersToHide == null || parametersToHide.Length == 0)
                return;

            HideParameters(operation, parametersToHide);
        }

        private static ApiParameterDescription[]? GetParametersToHide(OperationFilterContext context)
        {
            return context.ApiDescription?.ParameterDescriptions
                .Where(d => d.CustomAttributes()
                    .Any(a => a.GetType() == HidingAttributeType))
                .ToArray();
        }

        private static void HideParameters(OpenApiOperation operation, ApiParameterDescription[] parametersToHide)
        {
            foreach (var parameterToHide in parametersToHide)
            {
                var parameter = GetParameterByName(operation, parameterToHide.Name);
                if (parameter != null)
                    operation.Parameters.Remove(parameter);
            }
        }

        private static OpenApiParameter? GetParameterByName(OpenApiOperation operation, string name)
        {
            return operation.Parameters.FirstOrDefault(parameter =>
                string.Equals(parameter.Name, name, StringComparison.Ordinal));
        }
    }
}
