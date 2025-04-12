using Asp.Versioning;
using Microsoft.Extensions.Configuration;

namespace L.Heritage.Shared.Extensions.Core;

public static class OpenApiExtensions
{
    public static IHostApplicationBuilder AddDefaultOpenApi(
        this IHostApplicationBuilder builder,
        IApiVersioningBuilder? apiVersioning = default)
    {
        var openApi = builder.Configuration.GetSection("OpenApi");
        var identitySection = builder.Configuration.GetSection("Identity");

        var scopes = identitySection.Exists()
            ? identitySection.GetRequiredSection("Scopes")
                .GetChildren()
                .ToDictionary(p => p.Key, p => p.Value)
            : [];

        if (!openApi.Exists())
        {
            return builder;
        }

        if (apiVersioning is not null)
        {
            var versioned = apiVersioning.AddApiExplorer(options => options.GroupNameFormat = "'v'VVV");
            string[] versions = ["v1"];
            foreach (var description in versions)
            {
                builder.Services.AddOpenApi(description);
            }
        }

        return builder;
    }
}
