using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.WebApi;
using MediatR;
using Owin;
using Web_API_Spike.Configuration;
using Web_API_Spike.Controllers;

namespace Web_API_Spike
{
    public static class AutoFacConfig
    {
        public static Assembly[] Assemblies { get; set; }

        static AutoFacConfig()
        {
            Assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name.StartsWith("Web API Spike")
            ).ToArray();
        }

        public static void Register(HttpConfiguration config, IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Register all the Web API controllers with AutoFac
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register our types with AutoFac
            builder.RegisterType<Settings>()
                .As<ISettings>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SampleController>();

            // More complex mediator registration
            RegisterMediator(builder);

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
        }

        private static void RegisterMediator(ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).As<IMediator>().AsImplementedInterfaces();

            foreach (var handler in Assemblies.SelectMany(a => a.GetTypes())
                .Where(x => x.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IAsyncRequest<>)))))
            {
                builder.RegisterType(handler)
                    .AsImplementedInterfaces()
                    .InstancePerRequest();
            }

            foreach (var handler in Assemblies.SelectMany(a => a.GetTypes())
                .Where(x => x.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IRequest<>)))))
            {
                builder.RegisterType(handler)
                    .AsImplementedInterfaces()
                    .InstancePerRequest();
            }

            foreach (var handler in Assemblies.SelectMany(a => a.GetTypes())
                .Where(x => x.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IAsyncRequestHandler<,>)))))
            {
                builder.RegisterType(handler)
                    .AsImplementedInterfaces()
                    .InstancePerRequest();
            }

            foreach (var handler in Assemblies.SelectMany(a => a.GetTypes())
                .Where(x => x.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IRequestHandler<,>)))))
            {
                builder.RegisterType(handler)
                    .AsImplementedInterfaces()
                    .InstancePerRequest();
            }
        }
    }
}