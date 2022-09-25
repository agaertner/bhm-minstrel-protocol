using Nekres.Musician.Core.Domain;
using System.Threading;
using static Blish_HUD.Controls.Intern.GuildWarsControls;
namespace Nekres.Musician.Core.Instrument
{
    public class Bass : InstrumentBase
    {
        public Bass() : base(Octave.Low, true)
        {
        }

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new BassNote(WeaponSkill1, Octave.High)) && this.CurrentOctave == Octave.Low)
                note = new BassNote(UtilitySkill2, Octave.Low);
            else if (note.Equals(new BassNote(UtilitySkill2, Octave.Low)) && this.CurrentOctave == Octave.High)
                note = new BassNote(WeaponSkill1, Octave.High);
            return note;
        }

        protected override NoteBase ConvertNote(RealNote note) => BassNote.From(note);

        protected override void IncreaseOctave()
        {
            switch (this.CurrentOctave)
            {
                case Octave.Low:
                    this.CurrentOctave = Octave.High;
                    break;
                case Octave.High:
                    break;
                default: break;
            }

            PressKey(EliteSkill);

            Thread.Sleep(this.OctaveTimeout);
        }

        protected override void DecreaseOctave()
        {
            switch (this.CurrentOctave)
            {
                case Octave.Low:
                    break;
                case Octave.High:
                    this.CurrentOctave = Octave.Low;
                    break;
                default: break;
            }

            PressKey(UtilitySkill3);

            Thread.Sleep(this.OctaveTimeout);
        }
    }
}
