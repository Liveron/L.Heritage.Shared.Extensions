using Microsoft.AspNetCore.OpenApi;

namespace L.Heritage.Shared.Extensions.Core;

internal static class OpenApiOptionsExtensions
{
    internal static OpenApiOptions ApplyApiVersionInfo(this OpenApiOptions options, string title, string description)
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            return Task.CompletedTask;
        });
        return options;
    }
}
