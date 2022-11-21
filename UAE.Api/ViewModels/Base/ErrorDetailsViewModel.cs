using System.Text.Json;

namespace UAE.Api.ViewModels.Base;

public record ErrorDetailsViewModel
{
    public int StatusCode { get; init; }
    public string Message { get; init; }

    public override string ToString() => 
        JsonSerializer.Serialize(this);
}