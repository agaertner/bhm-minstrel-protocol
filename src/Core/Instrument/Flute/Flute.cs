using Nekres.Musician.Core.Domain;
using System.Threading;
using static Blish_HUD.Controls.Intern.GuildWarsControls;

namespace Nekres.Musician.Core.Instrument
{
    public class Flute : InstrumentBase
    {
        public Flute() : base(Octave.Low, false)
        {
        }

        protected override NoteBase ConvertNote(RealNote note) => FluteNote.From(note);

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new FluteNote(WeaponSkill1, Octave.High)) && CurrentOctave == Octave.Low)
                note = new FluteNote(UtilitySkill2, Octave.Low);
            else if (note.Equals(new FluteNote(UtilitySkill2, Octave.Low)) && CurrentOctave == Octave.High)
                note = new FluteNote(WeaponSkill1, Octave.High);
            return note;
        }

        protected override void IncreaseOctave()
        {
            switch (CurrentOctave)
            {
                case Octave.Low:
                    CurrentOctave = Octave.High;
                    break;
                case Octave.High:
                    break;
                default: break;
            }

            PressKey(UtilitySkill3);

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
