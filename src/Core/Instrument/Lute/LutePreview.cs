using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using static Blish_HUD.Controls.Intern.GuildWarsControls;

namespace Nekres.Musician.Core.Instrument
{
    internal class LutePreview : InstrumentBase
    {
        private readonly ISoundRepository _soundRepository;

        public LutePreview(ISoundRepository soundRepo) : base(Octave.Middle, true)
        {
            _soundRepository = soundRepo;
        }

        protected override NoteBase ConvertNote(RealNote note) => LuteNote.From(note);

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new LuteNote(WeaponSkill1, Octave.High)) && CurrentOctave == Octave.Middle)
                note = new LuteNote(UtilitySkill2, Octave.Middle);
            else if (note.Equals(new LuteNote(UtilitySkill2, Octave.Middle)) && CurrentOctave == Octave.High)
                note = new LuteNote(WeaponSkill1, Octave.High);
            else if (note.Equals(new LuteNote(WeaponSkill1, Octave.Middle)) && CurrentOctave == Octave.Low)
                note = new LuteNote(UtilitySkill2, Octave.Low);
            else if (note.Equals(new LuteNote(UtilitySkill2, Octave.Low)) && CurrentOctave == Octave.Middle)
                note = new LuteNote(WeaponSkill1, Octave.Middle);
            return note;
        }

        protected override void IncreaseOctave()
        {
            switch (this.CurrentOctave)
            {
                case Octave.Low:
                    this.CurrentOctave = Octave.Middle;
                    break;
                case Octave.Middle:
                    this.CurrentOctave = Octave.High;
                    break;
                case Octave.High:
                    break;
                default: break;
            }
        }

        protected override void DecreaseOctave()
        {
            switch (this.CurrentOctave)
            {
                case Octave.Low:
                    break;
                case Octave.Middle:
                    this.CurrentOctave = Octave.Low;
                    break;
                case Octave.High:
                    this.CurrentOctave = Octave.Middle;
                    break;
                default: break;
            }
        }

        protected override void PressKey(GuildWarsControls key)
        {
            switch (key)
            {
                case WeaponSkill1:
                case WeaponSkill2:
                case WeaponSkill3:
                case WeaponSkill4:
                case WeaponSkill5:
                case HealingSkill:
                case UtilitySkill1:
                case UtilitySkill2:
                    MusicianModule.ModuleInstance.MusicPlayer.PlaySound(_soundRepository.Get(key, this.CurrentOctave));
                    break;
                case UtilitySkill3:
                    DecreaseOctave();
                    break;
                case EliteSkill:
                    IncreaseOctave();
                    break;
                default: break;
            }
        }
    }
}
