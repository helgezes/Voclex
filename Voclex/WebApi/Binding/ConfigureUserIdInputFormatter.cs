using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

namespace WebApi.Binding
{
    public class ConfigureUserIdInputFormatter : IConfigureOptions<MvcOptions>
    {
        private readonly JsonOptions jsonOptions;
        private readonly ILoggerFactory loggerFactory;

        public ConfigureUserIdInputFormatter(IOptions<JsonOptions> jsonOptions, ILoggerFactory loggerFactory)
        {
            this.jsonOptions = jsonOptions.Value;
            this.loggerFactory = loggerFactory;
        }

        public void Configure(MvcOptions options)
        {
            var logger = loggerFactory.CreateLogger<SystemTextJsonInputFormatter>();
            options.InputFormatters.Insert(0, new UserIdInputFormatter(jsonOptions, logger));
        }
    }
}
