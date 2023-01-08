using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Play.Common.Settings;

namespace Play.Common.Logging
{
    public static class Extensions
    {
        public static IHostBuilder AddSeqLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging((hostingContext, loggingBuilder) =>
            {
                var configuration = hostingContext.Configuration;
                var seqSettings = configuration.GetSection(nameof(SeqSettings)).Get<SeqSettings>();
                loggingBuilder.AddSeq(serverUrl: seqSettings.ServerUrl);
            });            
        }

        public static IServiceCollection AddSeqLogging(this IServiceCollection services, IConfiguration config)
        {
            return services.AddLogging(loggingBuilder =>
           {
               var seqSettings = config.GetSection(nameof(SeqSettings)).Get<SeqSettings>();
               loggingBuilder.AddSeq(serverUrl: seqSettings.ServerUrl);
           });            
        }
    }
}