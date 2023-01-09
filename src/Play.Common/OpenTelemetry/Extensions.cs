using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Play.Common.MassTansit;
using Play.Common.Settings;

namespace Play.Common.OpenTelemetry
{
    public static class Extensions
    {
        public static IServiceCollection AddTracing(this IServiceCollection services, IConfiguration config){
            services.AddOpenTelemetryTracing(builder=>{

                var serviceSettings=config.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var jaegerSettings=config.GetSection(nameof(JaegerSettings)).Get<JaegerSettings>();

                builder.AddSource(serviceSettings.ServiceName)
                        .AddSource("MassTransit")
                        .SetResourceBuilder(
                            ResourceBuilder.CreateDefault()
                                            .AddService(serviceName: serviceSettings.ServiceName))
                        .AddHttpClientInstrumentation()
                        .AddAspNetCoreInstrumentation()
                        .AddJaegerExporter(options=>{
                            options.AgentHost=jaegerSettings.Host;
                            options.AgentPort=jaegerSettings.Port;
                        });

            })
            .AddConsumeObserver<ConsumeObserver>();

            return services;
        }
    }
}