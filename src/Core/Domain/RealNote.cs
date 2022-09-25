using System;
using System.Linq;

namespace Nekres.Musician.Core.Domain
{
    public enum Note
    {
        Z,
        C,
        D,
        E,
        F,
        G,
        A,
        B
    }

    public enum Octave
    {

        Lowest,
        Low,
        Middle,
        High,
        Highest
    }

    public class RealNote
    {
        public Note Note { get; }

        public Octave Octave { get; }

        public RealNote(Note note, Octave octave)
        {
            Note = note;
            Octave = octave;
        }

        public override string ToString()
        {
            return $"{(Octave > Octave.Middle ? "▲" : Octave < Octave.Middle ? "▼" : string.Empty)}{Note}";
        }

        public override bool Equals(object obj) => Equals((RealNote)obj);
        protected bool Equals(RealNote other) => Note == other.Note && Octave == other.Octave;

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Note*397) ^ (int)Octave;
            }
        }

        public string Serialize()
        {
            if (Note == Note.Z)
                return "z";

            var result = Note.ToString();

            switch (Octave)
            {
                case Octave.Lowest:
                    result += ",";
                    break;
                case Octave.Low:
                    result += ",,";
                    break;
                case Octave.Middle:
                    result = result.ToLowerInvariant();
                    break;
                case Octave.High:
                    result = $"{result.ToLowerInvariant()}'";
                    break;
                case Octave.Highest:
                    result = $"{result.ToLowerInvariant()}''";
                    break;
                default: break;
            }
            return result;
        }

        public static RealNote Deserialize(string str)
        {
            if (string.IsNullOrEmpty(str) || !Enum.TryParse<Note>(str[0].ToString(), true, out var key) 
                                          || !TryParseOctave(str, out var octave))
                throw new FormatException("Provided string is not valid.");
            return new RealNote(key, octave);
        }

        private static bool TryParseOctave(string text, out Octave octave)
        {
            octave = 0;
            if (string.IsNullOrEmpty(text) || !Array.Exists(Enum.GetValues(typeof(Note)).Cast<Note>().ToArray(), k => char.Parse(k.ToString()).Equals(char.ToUpperInvariant(text[0])))) 
                return false;

            if (text.Length == 1)
            {
                octave = char.IsUpper(char.Parse(text)) ? Octave.Low : Octave.Middle;
                return true;
            }

            var octaveMarks = text.Substring(1, text.Length - 1);

            switch (octaveMarks[0])
            {
                case ',':
                    switch (octaveMarks.Length)
                    {
                        case 1: octave = Octave.Lowest; break;
                        case 2: octave = Octave.Low; break;
                        case 3: octave = Octave.Middle; break;
                    }
                    return true;
                case '\'':
                    switch (octaveMarks.Length)
                    {
                        case 1: octave = Octave.High; break;
                        case 2: octave = Octave.Highest; break;
                    }
                    return true;
                default:
                    return false;
            }
        }
    }
}