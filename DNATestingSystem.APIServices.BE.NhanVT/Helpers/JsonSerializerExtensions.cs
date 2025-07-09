using System.Text.Json;

namespace DNATestingSystem.APIServices.BE.NhanVT.Helpers
{
    /// <summary>
    /// Extension methods for JSON serialization
    /// </summary>
    public static class JsonSerializerExtensions
    {
        /// <summary>
        /// Serializes an object to JSON using the application's configured JSON serializer options
        /// </summary>
        public static string ToJson(this object value)
        {
            // Create options that match the ASP.NET Core configured options
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
            
            // Add our virtual property converter
            options.Converters.Add(new JsonIgnoreVirtualMembersConverter());

            return JsonSerializer.Serialize(value, options);
        }
    }
}
