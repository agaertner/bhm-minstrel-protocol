using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using static Blish_HUD.Controls.Intern.GuildWarsControls;
namespace Nekres.Musician.Core.Instrument
{
    internal class FlutePreview : InstrumentBase
    {
        private readonly ISoundRepository _soundRepository;

        public FlutePreview(ISoundRepository soundRepo) : base(Octave.Low, true)
        {
            _soundRepository = soundRepo;
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
            switch (this.CurrentOctave)
            {
                case Octave.Low:
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
                case Octave.High:
                    this.CurrentOctave = Octave.Low;
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
                    MusicianModule.ModuleInstance.MusicPlayer.PlaySound(_soundRepository.Get(key, this.CurrentOctave), true);
                    break;
                case UtilitySkill3:
                    if (this.CurrentOctave == Octave.Low)
                        IncreaseOctave();
                    else
                        DecreaseOctave();
                    break;
                case EliteSkill:
                    MusicianModule.ModuleInstance.MusicPlayer.StopSound();
                    break;
                default: break;
            }
        }
    }
}
