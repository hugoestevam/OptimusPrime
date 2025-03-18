using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry;
using System;

namespace robot.Infra.Data
{
    public static class DataTelemetry
    {
        private static readonly Lazy<TracerProvider> _tracerProviderInstance = new Lazy<TracerProvider>(() => CreateTracerProvider());

        public static TracerProvider Instance => _tracerProviderInstance.Value;

        private static TracerProvider CreateTracerProvider()
        {
            return Sdk.CreateTracerProviderBuilder()
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService("RobotInfraData", serviceVersion: "1.0.0"))
                .AddSource("RobotInfraData")
                .AddOtlpExporter(otlpOptions =>
                {
                    otlpOptions.Endpoint = new Uri("http://localhost:4317");
                })
                .Build();
        }
    }
}
