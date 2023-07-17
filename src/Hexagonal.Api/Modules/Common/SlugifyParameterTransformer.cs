namespace hexagonal.API.Modules.Common;

/// <summary>
///     SlugifyParameterTransformer
/// </summary>
public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    /// <summary>
    ///     TransformOutbound
    /// </summary>
    /// <param name="value"></param>
    public string? TransformOutbound(object? value)
    {
        var result = Regex.Replace(value?.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2")
            .ToLower(CultureInfo.CurrentCulture);

        return value == null ? null : result;
    }
}