using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.Owin;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Web_API_Spike.Startup))]

namespace Web_API_Spike
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            AutoFacConfig.Register(config, app);

            ConfigureAuth(app);

            var resolver = new DefaultInlineConstraintResolver();

            config.MapHttpAttributeRoutes(resolver);

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            app.UseWebApi(config);
        }
    }
}
