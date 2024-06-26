﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using Branef.Framework.Logging;
using Branef.Framework.Exceptions;

namespace Branef.Framework.Logging
{
    public static class LoggingExtensions
    {
        public static IHostBuilder ConfigureLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging((hostContext, configLogging) =>
            {
                configLogging.AddSerilog(hostContext.Configuration);
            });
        }

        public static ILoggingBuilder AddSerilog(this ILoggingBuilder configLogging, IConfiguration configuration)
        {
            configLogging.Services.Configure<SeqOptions>(configuration.GetSection("Seq"));
            var seqOptions = configuration.GetSection("Seq").Get<SeqOptions>()!;

            var minimumLevel = configuration.GetValue<LogLevel>("Logging:LogLevel:Default");

            Log.Logger = new LoggerConfiguration()
                    .Enrich.WithProperty("Application", System.Reflection.Assembly.GetEntryAssembly()!.GetName().Name!)
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.Conditional(
                        logEvent =>
                            logEvent.Exception is not BusinessException ||
                            logEvent.Exception is BusinessException businessException && businessException.Errors.Any(error => error.Type == ErrorType.Unhandled),
                        config =>
                            config.Seq(
                                serverUrl: seqOptions.Address.ToString(),
                                eventBodyLimitBytes: seqOptions.EventBodyLimitBytes))
                    .MinimumLevel.Is((Serilog.Events.LogEventLevel)minimumLevel)
                    .CreateLogger();

            configLogging.ClearProviders();
            configLogging.AddSerilog(dispose: true);

            return configLogging;
        }
    }
}
