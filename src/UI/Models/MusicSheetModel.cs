using Nekres.Musician.Core.Models;
using System;

namespace Nekres.Musician.UI.Models
{
    internal class MusicSheetModel
    {
        [LiteDB.BsonField("id")]
        public Guid Id { get; set; }

        [LiteDB.BsonField("artist")]
        public string Artist { get; set; }

        [LiteDB.BsonField("title")]
        public string Title { get; set; }

        [LiteDB.BsonField("user")]
        public string User { get; set; }

        [LiteDB.BsonField("instrument")]
        public Instrument Instrument { get; set; }

        [LiteDB.BsonField("tempo")]
        public string Tempo { get; set; }

        [LiteDB.BsonField("algorithm")]
        public Algorithm Algorithm { get; set; }

        [LiteDB.BsonField("melody")]
        public string Melody { get; set; }
    }
}
