using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using static Blish_HUD.Controls.Intern.GuildWarsControls;
namespace Nekres.Musician.Core.Instrument
{
    internal class BassPreview : InstrumentBase
    {
        private readonly ISoundRepository _soundRepository;

        public BassPreview(ISoundRepository soundRepo) : base(Octave.Low, true)
        {
            _soundRepository = soundRepo;
        }

        protected override NoteBase ConvertNote(RealNote note) => BassNote.From(note);

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new BassNote(WeaponSkill1, Octave.High)) && CurrentOctave == Octave.Low)
                note = new BassNote(UtilitySkill2, Octave.Low);
            else if (note.Equals(new BassNote(UtilitySkill2, Octave.Low)) && CurrentOctave == Octave.High)
                note = new BassNote(WeaponSkill1, Octave.High);
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
                    MusicianModule.ModuleInstance.MusicPlayer.PlaySound(_soundRepository.Get(key, CurrentOctave));
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
