using Nekres.Musician.Core.Domain;
using System;
using System.Threading;
using static Blish_HUD.Controls.Intern.GuildWarsControls;

namespace Nekres.Musician.Core.Instrument
{
    public class Harp : InstrumentBase
    {
        public Harp() : base(Octave.Middle, false)
        {
        }

        protected override NoteBase ConvertNote(RealNote note) => HarpNote.From(note);

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new HarpNote(WeaponSkill1, Octave.Middle)) && CurrentOctave == Octave.Low)
                note = new HarpNote(UtilitySkill2, Octave.Low);
            else if (note.Equals(new HarpNote(WeaponSkill1, Octave.High)) && CurrentOctave == Octave.Middle)
                note = new HarpNote(UtilitySkill2, Octave.Middle);
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
                default: break;
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
                default: break;
            }

            PressKey(UtilitySkill3);

            Thread.Sleep(OctaveTimeout);
        }
    }
}
