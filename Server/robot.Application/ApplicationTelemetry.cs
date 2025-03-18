using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry;
using System;
using OpenTelemetry.Logs;

namespace robot.Application
{
    public static class ApplicationTelemetry
    {
        private static readonly Lazy<TracerProvider> _tracerProviderInstance = new Lazy<TracerProvider>(() => CreateTracerProvider());

        public static TracerProvider Instance => _tracerProviderInstance.Value;

        private static TracerProvider CreateTracerProvider()
        {
            return Sdk.CreateTracerProviderBuilder()
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService("RobotHandler", serviceVersion: "1.0.0"))
                .AddSource("RobotHandler")
                .AddOtlpExporter(otlpOptions =>
                {
                    otlpOptions.Endpoint = new Uri("http://localhost:4317");
                })
                .Build();
        }
    }
}
