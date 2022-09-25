using System;
using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using static Blish_HUD.Controls.Intern.GuildWarsControls;
namespace Nekres.Musician.Core.Instrument
{
    internal class HarpPreview : InstrumentBase
    {
        private readonly ISoundRepository _soundRepository;

        public HarpPreview(ISoundRepository soundRepo) : base(Octave.Middle, true)
        {
            _soundRepository = soundRepo;
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
                case Octave.Middle:
                    this.CurrentOctave = Octave.Low;
                    break;
                case Octave.High:
                    this.CurrentOctave = Octave.Middle;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
