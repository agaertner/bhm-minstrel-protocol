using System;
using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using static Blish_HUD.Controls.Intern.GuildWarsControls;
namespace Nekres.Musician.Core.Instrument
{
    internal class Bell2Preview : InstrumentBase
    {
        private readonly ISoundRepository _soundRepository;

        public Bell2Preview(ISoundRepository soundRepo) : base(Octave.Low, true)
        {
            _soundRepository = soundRepo;
        }

        protected override NoteBase ConvertNote(RealNote note) => Bell2Note.From(note);

        protected override NoteBase OptimizeNote(NoteBase note)
        {
            if (note.Equals(new Bell2Note(WeaponSkill1, Octave.High)) && CurrentOctave == Octave.Low)
                note = new Bell2Note(UtilitySkill2, Octave.Low);
            else if (note.Equals(new Bell2Note(UtilitySkill2, Octave.Low)) && CurrentOctave == Octave.High)
                note = new Bell2Note(WeaponSkill1, Octave.High);
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
                default:
                    throw new ArgumentOutOfRangeException();
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
                case GuildWarsControls.WeaponSkill1:
                case GuildWarsControls.WeaponSkill2:
                case GuildWarsControls.WeaponSkill3:
                case GuildWarsControls.WeaponSkill4:
                case GuildWarsControls.WeaponSkill5:
                case GuildWarsControls.HealingSkill:
                case GuildWarsControls.UtilitySkill1:
                case GuildWarsControls.UtilitySkill2:
                    MusicianModule.ModuleInstance.MusicPlayer.PlaySound(_soundRepository.Get(key, this.CurrentOctave));
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
