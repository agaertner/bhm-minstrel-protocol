using System;
using Blish_HUD;

namespace Nekres.Musician.Core.Domain
{
    public class Metronome
    {
        public int Tempo { get; }
        public Fraction BeatsPerMeasure { get; }
        public TimeSpan QuaterNoteLength { get; }
        public TimeSpan WholeNoteLength { get; }

        public Metronome(int tempo, Fraction beatsPerMeasure)
        {
            BeatsPerMeasure = beatsPerMeasure;
            Tempo = tempo;

            var divisor = beatsPerMeasure.Denominator != 0 ? tempo * 16 / beatsPerMeasure.Denominator : tempo * 16;

            QuaterNoteLength = TimeSpan.FromMinutes(1);

            if (divisor != 0) QuaterNoteLength = QuaterNoteLength.Divide(divisor);

            WholeNoteLength = TimeSpan.FromMinutes(1);

            if (divisor != 0) WholeNoteLength = WholeNoteLength.Divide(divisor);

            WholeNoteLength = WholeNoteLength.Multiply(4);
        }

        public override string ToString() => $"{Tempo} {BeatsPerMeasure}";

        public static Metronome FromString(string s)
        {
            var val = s.Split(' ');
            if (val.Length < 2) return null;
            var fraction = val[1].Split('/');
            return new Metronome(int.Parse(val[0]), new Fraction(int.Parse(fraction[0]), int.Parse(fraction[1])));
        }
    }
}