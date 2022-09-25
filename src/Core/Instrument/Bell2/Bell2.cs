using Nekres.Musician.Core.Domain;
using System;
using System.Threading;
using static Blish_HUD.Controls.Intern.GuildWarsControls;

namespace Nekres.Musician.Core.Instrument
{
    public class Bell2 : InstrumentBase
    {
        public Bell2() : base(Octave.Low, true)
        {
        }

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new Bell2Note(WeaponSkill1, Octave.High)) && CurrentOctave == Octave.Low)
                note = new Bell2Note(UtilitySkill2, Octave.Low);
            else if (note.Equals(new Bell2Note(UtilitySkill2, Octave.Low)) && CurrentOctave == Octave.High)
                note = new Bell2Note(WeaponSkill1, Octave.High);
            return note;
        }

        protected override NoteBase ConvertNote(RealNote note) => Bell2Note.From(note);

        protected override void IncreaseOctave()
        {
            switch (CurrentOctave)
            {
                case Octave.Low:
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
                case Octave.High:
                    CurrentOctave = Octave.Low;
                    break;
                default: break;
            }

            PressKey(UtilitySkill3);

            Thread.Sleep(OctaveTimeout);
        }
    }
}
