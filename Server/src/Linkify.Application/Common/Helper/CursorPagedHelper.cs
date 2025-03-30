using Linkify.Domain.Shared;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Linkify.Application.Common.Helper
{
    public static class CursorPagedHelper
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            Converters = { new JsonStringEnumConverter() }
        };

        /// <summary>
        /// Encode cursor dictionary to Base64 JSON string.
        /// </summary>
        public static string? EncodeCursor(Dictionary<string, object> cursorValues)
        {
            if (cursorValues == null || cursorValues.Count == 0)
                return null;

            var serializableValues = cursorValues.ToDictionary(
                kv => kv.Key,
                kv => kv.Value switch
                {
                    DateTime dt => dt.ToString("O"),        // ISO 8601 format
                    DateTimeOffset dto => dto.ToString("O"), // ISO 8601 format
                    Guid guid => guid.ToString(),           // Store as string
                    _ => kv.Value
                });

            var json = JsonSerializer.Serialize(serializableValues, _jsonOptions);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }

        /// <summary>
        /// Decode Base64 JSON cursor string to a dictionary with proper type conversion.
        /// </summary>
        public static Dictionary<string, object> DecodeCursor(string? cursor)
        {
            if (string.IsNullOrEmpty(cursor))
                return [];

            try
            {
                var bytes = Convert.FromBase64String(cursor);
                var json = Encoding.UTF8.GetString(bytes);
                var rawDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, _jsonOptions) ?? [];

                var convertedDict = new Dictionary<string, object>();

                foreach (var (key, jsonElement) in rawDict)
                {
                    convertedDict[key] = ConvertJsonElement(jsonElement);
                }

                return convertedDict;
            }
            catch
            {
                return [];
            }
        }

        /// <summary>
        /// Extracts cursor values from entity based on sorting criteria.
        /// </summary>
        public static string? CreateCursor<TEntity>(TEntity entity, IEnumerable<SortCriteria> sortCriteriaList)
        {
            if (entity == null || sortCriteriaList == null)
                return null;

            var cursorValues = new Dictionary<string, object>();

            foreach (var criteria in sortCriteriaList)
            {
                var property = typeof(TEntity).GetProperty(criteria.FieldName, BindingFlags.Public | BindingFlags.Instance);
                if (property == null) continue;

                var value = property.GetValue(entity);
                if (value != null)
                {
                    cursorValues[criteria.FieldName] = value;
                }
            }

            return EncodeCursor(cursorValues);
        }

        /// <summary>
        /// Convert JsonElement to the appropriate C# type.
        /// </summary>
        private static object ConvertJsonElement(JsonElement jsonElement)
        {
            return jsonElement.ValueKind switch
            {
                JsonValueKind.String when Guid.TryParse(jsonElement.GetString(), out var guid) => guid,
                JsonValueKind.String when DateTime.TryParse(jsonElement.GetString(), out var dateTime) => dateTime,
                JsonValueKind.String when DateTimeOffset.TryParse(jsonElement.GetString(), out var dateTimeOffset) => dateTimeOffset,
                JsonValueKind.String => jsonElement.GetString()!,
                JsonValueKind.Number when jsonElement.TryGetInt64(out var longValue) => longValue,
                JsonValueKind.Number => jsonElement.GetDouble(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null!,
                _ => jsonElement
            };
        }
    }
}