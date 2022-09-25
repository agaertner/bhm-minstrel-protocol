using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nekres.Musician.Core.Domain
{
    public class ChordOffset
    {
        private static readonly Regex NonWhitespace = new(@"[^\s]+");
        public ChordOffset(Chord chord, Beat offset)
        {
            Chord = chord;
            Offset = offset;
        }

        public Chord Chord { get; }
        public Beat Offset { get; }

        public override string ToString() => Chord.ToString();

        public static IEnumerable<ChordOffset> MelodyFromString(string s)
        {
            var currentBeat = 0m;

            return NonWhitespace.Matches(s).Cast<Match>().Select(textChord =>
            {
                var chord = Chord.Parse(textChord.Value);

                var chordOffset = new ChordOffset(chord, new Beat(currentBeat));

                currentBeat += chord.Length;

                return chordOffset;
            });
        }
    }
}