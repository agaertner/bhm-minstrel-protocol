using System;
using Newtonsoft.Json;

namespace Nekres.Musician.Core.Domain
{
    public class MetronomeConverter : JsonConverter<Metronome>
    {
        public override void WriteJson(JsonWriter writer, Metronome value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value.Tempo} {value.BeatsPerMeasure.Nominator}/{value.BeatsPerMeasure.Denominator}");
        }

        public override Metronome ReadJson(JsonReader reader, Type objectType, Metronome existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return reader.Value == null ? null : Metronome.FromString((string)reader.Value);
        }
    }
}