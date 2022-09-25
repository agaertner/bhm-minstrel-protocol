using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nekres.Musician.Core.Domain
{
    public class Chord
    {
        private static readonly Regex NotesAndDurationRegex = new Regex(@"\[?([ABCDEFGZabcdefgz',]+)\]?(\d+)?\/?(\d+)?");
        private static readonly Regex NoteRegex = new Regex(@"([ABCDEFGZabcdefgz][,]{0,3}[']{0,2})");

        private Chord(IEnumerable<RealNote> notes, Fraction length)
        {
            Length = length;
            Notes = notes;
        }

        public Fraction Length { get; }

        public IEnumerable<RealNote> Notes { get; }

        public static Chord Parse(string text)
        {
            var notesAndDuration = NotesAndDurationRegex.Match(text);

            // All notes (either between [ and ] or not) making up the chord
            var chord = notesAndDuration.Groups[1].Value;

            // Followed by the duration fraction
            var nominator = notesAndDuration.Groups[2].Value;
            var denominator = notesAndDuration.Groups[3].Value;

            // Notes inside the chord
            var notes = NoteRegex.Matches(chord);
            
            // Parse the duration
            var length = Fraction.Parse(nominator, denominator);

            // Return the chord
            return new Chord(notes.Cast<Match>().Select(x => RealNote.Deserialize(x.Groups[1].Value)), length);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            if (Notes.Count() > 1)
            {
                stringBuilder.Append("[");
            }

            foreach (var note in Notes)
            {
                stringBuilder.Append(note.Serialize());
            }

            if (Notes.Count() > 1)
            {
                stringBuilder.Append("]");
            }

            if (Length.Nominator != 1)
            {
                stringBuilder.Append(Length.Nominator);
            }

            if (Length.Denominator != 1)
            {
                stringBuilder.Append("/");
                stringBuilder.Append(Length.Denominator);
            }

            return stringBuilder.ToString();
        }
    }
}