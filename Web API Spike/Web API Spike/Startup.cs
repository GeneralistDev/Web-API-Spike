using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using FluentValidation.WebApi;
using Microsoft.Owin;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using Web_API_Spike.Configuration;
using Web_API_Spike.Configuration.Filters;

[assembly: OwinStartup(typeof(Web_API_Spike.Startup))]

namespace Web_API_Spike
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            FluentValidationModelValidatorProvider.Configure(config);

            AutoFacConfig.Register(config, app);

            ConfigureAuth(app);

            config.Filters.Add(new ValidateModelStateFilter());
            config.MessageHandlers.Add(new ResponseWrappingHandler());

            var resolver = new DefaultInlineConstraintResolver();
            config.MapHttpAttributeRoutes(resolver);

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            app.UseWebApi(config);
        }
    }
}
