using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using System.Threading;

namespace Nekres.Musician.Core.Instrument
{
    public class Lute : InstrumentBase
    {
        public Lute() : base(Octave.Low, true)
        {
        }

        protected override NoteBase ConvertNote(RealNote note) => LuteNote.From(note);

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new LuteNote(GuildWarsControls.WeaponSkill1, Octave.High)) && CurrentOctave == Octave.Middle)
                note = new LuteNote(GuildWarsControls.UtilitySkill2, Octave.Middle);
            else if (note.Equals(new LuteNote(GuildWarsControls.UtilitySkill2, Octave.Middle)) && CurrentOctave == Octave.High)
                note = new LuteNote(GuildWarsControls.WeaponSkill1, Octave.High);
            else if (note.Equals(new LuteNote(GuildWarsControls.WeaponSkill1, Octave.Middle)) && CurrentOctave == Octave.Low)
                note = new LuteNote(GuildWarsControls.UtilitySkill2, Octave.Low);
            else if (note.Equals(new LuteNote(GuildWarsControls.UtilitySkill2, Octave.Low)) && CurrentOctave == Octave.Middle)
                note = new LuteNote(GuildWarsControls.WeaponSkill1, Octave.Middle);
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

            PressKey(GuildWarsControls.EliteSkill);

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

            PressKey(GuildWarsControls.UtilitySkill3);

            Thread.Sleep(OctaveTimeout);
        }
    }
}
