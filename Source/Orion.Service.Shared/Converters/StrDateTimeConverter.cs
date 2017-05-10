using System;
using System.Diagnostics;
using System.Globalization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Orion.Service.Shared.Converters
{
    public class StrDateTimeConverter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value.ToString();
            Debug.WriteLine("");
            if (DateTime.TryParseExact(value, "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime d))
                return d;
            return DateTime.MinValue;
        }
    }
}