using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using static Blish_HUD.Controls.Intern.GuildWarsControls;
namespace Nekres.Musician.Core.Instrument
{
    internal class HornPreview : InstrumentBase
    {
        private readonly ISoundRepository _soundRepository;

        public HornPreview(ISoundRepository soundRepo) : base(Octave.Middle, true)
        {
            _soundRepository = soundRepo;
        }

        protected override NoteBase ConvertNote(RealNote note) => HornNote.From(note);

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new HornNote(WeaponSkill1, Octave.High)) && CurrentOctave == Octave.Middle)
                note = new HornNote(UtilitySkill2, Octave.Middle);
            else if (note.Equals(new HornNote(UtilitySkill2, Octave.Middle)) && CurrentOctave == Octave.High)
                note = new HornNote(WeaponSkill1, Octave.High);
            else if (note.Equals(new HornNote(WeaponSkill1, Octave.Middle)) && CurrentOctave == Octave.Low)
                note = new HornNote(UtilitySkill2, Octave.Low);
            else if (note.Equals(new HornNote(UtilitySkill2, Octave.Low)) && CurrentOctave == Octave.Middle)
                note = new HornNote(WeaponSkill1, Octave.Middle);
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
                case GuildWarsControls.WeaponSkill1:
                case GuildWarsControls.WeaponSkill2:
                case GuildWarsControls.WeaponSkill3:
                case GuildWarsControls.WeaponSkill4:
                case GuildWarsControls.WeaponSkill5:
                case GuildWarsControls.HealingSkill:
                case GuildWarsControls.UtilitySkill1:
                case GuildWarsControls.UtilitySkill2:
                    MusicianModule.ModuleInstance.MusicPlayer.PlaySound(_soundRepository.Get(key, this.CurrentOctave), true);
                    break;
                case GuildWarsControls.UtilitySkill3:
                    DecreaseOctave();
                    break;
                case GuildWarsControls.EliteSkill:
                    IncreaseOctave();
                    break;
                default: break;
            }
        }
    }
}
