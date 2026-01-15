using System.Globalization;
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
            options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverterConfiguration());
        });

        return services;
    }

    // =========================
    // DateOnly
    // =========================
    internal class DateOnlyJsonConverterConfiguration : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(
                reader.GetString()!,
                Format,
                CultureInfo.InvariantCulture
            );
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }

    // =========================
    // TimeOnly
    // =========================
    internal class TimeOnlyJsonConverterConfiguration : JsonConverter<TimeOnly>
    {
        private const string Format = "HH:mm";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeOnly.ParseExact(
                reader.GetString()!,
                Format,
                CultureInfo.InvariantCulture
            );
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
