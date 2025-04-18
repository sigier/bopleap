using BopLeap.Core.Models;
using System.Text.Json.Serialization;


namespace BopLeap.Core.Json
{
    [JsonSourceGenerationOptions(WriteIndented = false)]
    [JsonSerializable(typeof(Trade))]
    public partial class BinanceJsonContext : JsonSerializerContext
    {
    }
}
