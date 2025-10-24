using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.CrossCuttingConcerns.Loggers.Serilog.Base;

public static class JsonOptionsCache
{
    public static readonly JsonSerializerOptions Default = new()
    {
        Encoder = JavaScriptEncoder.Default,
        WriteIndented = true,
        IgnoreReadOnlyProperties = false,
        IncludeFields = false,
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };
}