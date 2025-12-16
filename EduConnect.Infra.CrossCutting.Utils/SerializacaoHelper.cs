using System.Text.Json;

namespace EduConnect.Infra.CrossCutting.Utils;

public static class SerializacaoHelper
{
    public static string Serializar(this object obj)
    {
        if (obj == null) return string.Empty;
        return JsonSerializer.Serialize(obj, JsonSerializerOptions.Web);
    }

    public static T? Desserializar<T>(this string obj)
    {
        if (string.IsNullOrEmpty(obj)) return default;
        return JsonSerializer.Deserialize<T>(obj, JsonSerializerOptions.Web);
    }
}
