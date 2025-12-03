using System.Text.Json;
using System.Text.Json.Serialization;

namespace EduConnect.MiddleWares;

public static class JsonConverterConfiguration
{
    public static IServiceCollection AddJsonConverterConfiguration(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverterConfiguration());
        });
        return services;
    }

    internal class DateOnlyJsonConverterConfiguration : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString()!, Format);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
