using Nekres.Musician.Core.Domain;
using Nekres.Musician.Core.Domain.Converters;
using Nekres.Musician.UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Nekres.Musician.Core.Models
{
    public enum Algorithm
    {
        [EnumMember(Value = "favor-notes")]
        FavorNotes,
        [EnumMember(Value = "favor-chords")]
        FavorChords
    }

    public enum Instrument
    {
        [EnumMember(Value = "bass")]
        Bass,
        [EnumMember(Value = "bell")]
        Bell,
        [EnumMember(Value = "bell2")]
        Bell2,
        [EnumMember(Value = "flute")]
        Flute,
        [EnumMember(Value = "harp")]
        Harp,
        [EnumMember(Value = "horn")]
        Horn,
        [EnumMember(Value = "lute")]
        Lute
    }
    internal class MusicSheet
    {
        [JsonProperty("guid")]
        public Guid Id { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("instrument"), JsonConverter(typeof(StringEnumConverter))]
        public Instrument Instrument { get; set; }

        [JsonProperty("tempo"), JsonConverter(typeof(MetronomeConverter))]
        public Metronome Tempo { get; set; }

        [JsonProperty("algorithm"), JsonConverter(typeof(StringEnumConverter))]
        public Algorithm Algorithm { get; set; }

        [JsonProperty("melody"), JsonConverter(typeof(ChordOffsetConverter))]
        public IEnumerable<ChordOffset> Melody { get; set; }

        public MusicSheet()
        {
            Id = Guid.NewGuid();
        }

        public MusicSheetModel ToModel()
        {
            return new MusicSheetModel
            {
                Id = this.Id,
                Artist = this.Artist,
                Title = this.Title,
                User = this.User,
                Instrument = this.Instrument,
                Algorithm = this.Algorithm,
                Melody = string.Join(" ", this.Melody.Select(c => c.ToString())),
                Tempo = this.Tempo.ToString()
            };
        }

        public static MusicSheet FromModel(MusicSheetModel model)
        {
            return new MusicSheet
            {
                Id = model.Id,
                Artist = model.Artist,
                Title = model.Title,
                User = model.User,
                Instrument = model.Instrument,
                Algorithm = model.Algorithm,
                Melody = ChordOffset.MelodyFromString(model.Melody),
                Tempo = Metronome.FromString(model.Tempo),
            };
        }

        private static MusicSheet FromXml(XDocument xDocument)
        {
            var title = xDocument.Elements().SingleOrDefault()?.Elements("title").SingleOrDefault()?.Value ?? "???";
            var artist = xDocument.Elements().SingleOrDefault()?.Elements("artist").SingleOrDefault()?.Value ?? "Unknown Artist";
            var user = xDocument.Elements().SingleOrDefault()?.Elements("user").SingleOrDefault()?.Value ?? string.Empty;

            if (!Enum.TryParse<Instrument>(xDocument.Elements().SingleOrDefault()?.Elements("instrument").SingleOrDefault()?.Value, true, out var instrument))
                return null;

            var tempo = xDocument.Elements().SingleOrDefault()?.Elements("tempo").SingleOrDefault()?.Value;
            var meter = xDocument.Elements().SingleOrDefault()?.Elements("meter").SingleOrDefault()?.Value;

            if (string.IsNullOrEmpty(tempo) || string.IsNullOrEmpty(meter))
                return null;

            var melody = xDocument.Elements().SingleOrDefault()?.Elements("melody").SingleOrDefault()?.Value;
            if (string.IsNullOrEmpty(melody))
                return null;

            if (!Enum.TryParse<Algorithm>(xDocument.Elements().SingleOrDefault()?.Elements("algorithm").Single().Value.Replace(" ", string.Empty), true, out var algorithm))
                return null;

            return new MusicSheet
            {
                Title = title,
                Artist = artist,
                User = user,
                Instrument = instrument,
                Tempo = Metronome.FromString($"{tempo} {meter}"),
                Melody = ChordOffset.MelodyFromString(melody),
                Algorithm = algorithm
            };
        }

        public static MusicSheet FromXml(string path)
        {
            var timeout = DateTime.UtcNow.AddMilliseconds(FileUtil.FileTimeOutMilliseconds);
            while (DateTime.UtcNow < timeout)
            {
                try
                {
                    return FromXml(XDocument.Load(path));
                }
                catch (Exception e) when (e is IOException or UnauthorizedAccessException)
                {
                    if (DateTime.UtcNow < timeout) continue;
                    MusicianModule.Logger.Warn(e, e.Message);
                    break;
                }
                catch (Exception e) when (e is XmlException or FormatException)
                {
                    MusicianModule.Logger.Info(e, $"Invalid format or corrupted data: {Path.GetFileName(path)}");
                    break;
                }
            }
            return null;
        }

        public static bool TryParseXml(string xml, out MusicSheet sheet)
        {
            sheet = null;
            if (string.IsNullOrEmpty(xml)) return false;
            try
            {
                sheet = FromXml(XDocument.Parse(xml));
                return true;
            }
            catch (XmlException e)
            {
                MusicianModule.Logger.Warn(e, e.Message);
            }
            return false;
        }
    }
}