using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nekres.Musician.Core.Domain.Converters
{
    internal class ChordOffsetConverter : JsonConverter<IEnumerable<ChordOffset>>
    {
        public override void WriteJson(JsonWriter writer, IEnumerable<ChordOffset> value, JsonSerializer serializer)
        {
            var stringBuilder = new StringBuilder();

            foreach (var chordOffset in value)
            {
                stringBuilder.Append(" ");
                stringBuilder.Append(chordOffset.ToString());
            }
            writer.WriteValue(stringBuilder.ToString());
        }

        public override IEnumerable<ChordOffset> ReadJson(JsonReader reader, Type objectType, IEnumerable<ChordOffset> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.Value == null ? null : ChordOffset.MelodyFromString((string)reader.Value);
        }
    }
}
