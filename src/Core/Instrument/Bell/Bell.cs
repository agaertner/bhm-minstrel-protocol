using Nekres.Musician.Core.Domain;
using System;
using System.Threading;
using static Blish_HUD.Controls.Intern.GuildWarsControls;

namespace Nekres.Musician.Core.Instrument
{
    public class Bell : InstrumentBase
    {
        public Bell() : base(Octave.Middle, true)
        {
        }

        protected override NoteBase ConvertNote(RealNote note) => BellNote.From(note);

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new BellNote(WeaponSkill1, Octave.High)) && CurrentOctave == Octave.Middle)
                note = new BellNote(UtilitySkill2, Octave.Middle);
            else if (note.Equals(new BellNote(UtilitySkill2, Octave.Middle)) && CurrentOctave == Octave.High)
                note = new BellNote(WeaponSkill1, Octave.High);
            else if (note.Equals(new BellNote(WeaponSkill1, Octave.Middle)) && CurrentOctave == Octave.Low)
                note = new BellNote(UtilitySkill2, Octave.Low);
            else if (note.Equals(new BellNote(UtilitySkill2, Octave.Low)) && CurrentOctave == Octave.Middle)
                note = new BellNote(WeaponSkill1, Octave.Middle);
            return note;
        }

        protected override void IncreaseOctave()
        {
            switch (CurrentOctave)
            {
                case Octave.Low:
                    CurrentOctave = Octave.Middle;
                    break;
                case Octave.Middle:
                    CurrentOctave = Octave.High;
                    break;
                case Octave.High:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            PressKey(EliteSkill);

            Thread.Sleep(OctaveTimeout);
        }

        protected override void DecreaseOctave()
        {
            switch (CurrentOctave)
            {
                case Octave.Low:
                    break;
                case Octave.Middle:
                    CurrentOctave = Octave.Low;
                    break;
                case Octave.High:
                    CurrentOctave = Octave.Middle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            PressKey(UtilitySkill3);

            Thread.Sleep(OctaveTimeout);
        }
    }
}
