using Api.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Api;

public static class MiddlewaresRegister
{
    public static WebApplication SetupMiddleware(this WebApplication app)
    {
        // reference: https://andrewlock.net/deploying-asp-net-core-applications-to-kubernetes-part-6-adding-health-checks-with-liveness-readiness-and-startup-probes/
        // health check endpoints for external use
        app.MapHealthChecks("health/liveness", new HealthCheckOptions { Predicate = _ => false });

        // enable swagger in dev mode
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // enable streaming
        app.UseStreaming();

        app.MapControllers();

        return app;
    }
}