using System.Collections;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DNATestingSystem.APIServices.BE.NhanVT.Helpers
{
    /// <summary>
    /// JSON converter that excludes all virtual properties (navigation properties in EF Core)
    /// </summary>
    public class JsonIgnoreVirtualMembersConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            // Only convert complex types
            return typeToConvert.IsClass 
                && typeToConvert != typeof(string) 
                && !typeToConvert.IsPrimitive;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(VirtualMemberIgnoringConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
    }

    /// <summary>
    /// Converter that excludes virtual properties during serialization
    /// </summary>
    public class VirtualMemberIgnoringConverter<T> : JsonConverter<T> where T : class
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Use default deserialization
            var newOptions = new JsonSerializerOptions(options);
            RemoveConverter(newOptions);
            return JsonSerializer.Deserialize<T>(ref reader, newOptions);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            if (value is IEnumerable enumerable && value.GetType() != typeof(string))
            {
                WriteEnumerable(writer, enumerable, options);
                return;
            }

            writer.WriteStartObject();

            // Get all properties, filtering out virtual ones
            foreach (var property in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                // Skip if property has [JsonIgnore] attribute
                if (property.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                {
                    continue;
                }

                // Skip if property is virtual (navigation property)
                if (property.GetGetMethod()?.IsVirtual == true)
                {
                    continue;
                }

                try
                {
                    var propertyValue = property.GetValue(value);
                    string propertyName = options.PropertyNamingPolicy?.ConvertName(property.Name) ?? property.Name;
                    
                    writer.WritePropertyName(propertyName);
                    
                    // Create a copy of options without this converter to avoid stack overflow
                    var newOptions = new JsonSerializerOptions(options);
                    RemoveConverter(newOptions);
                    
                    JsonSerializer.Serialize(writer, propertyValue, newOptions);
                }
                catch
                {
                    // Skip properties that cause errors (e.g., lazy loading proxies)
                    continue;
                }
            }

            writer.WriteEndObject();
        }

        private void WriteEnumerable(Utf8JsonWriter writer, IEnumerable items, JsonSerializerOptions options)
        {
            var newOptions = new JsonSerializerOptions(options);
            RemoveConverter(newOptions);

            writer.WriteStartArray();

            foreach (var item in items)
            {
                JsonSerializer.Serialize(writer, item, item?.GetType() ?? typeof(object), newOptions);
            }

            writer.WriteEndArray();
        }

        private void RemoveConverter(JsonSerializerOptions options)
        {
            // Remove this converter to avoid infinite recursion
            for (int i = options.Converters.Count - 1; i >= 0; i--)
            {
                if (options.Converters[i] is JsonIgnoreVirtualMembersConverter)
                {
                    options.Converters.RemoveAt(i);
                }
            }
        }
    }
}
