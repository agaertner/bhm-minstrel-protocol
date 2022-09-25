using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using System.Collections.Generic;

namespace Nekres.Musician.Core.Instrument
{
    public class HornNote : NoteBase
    {
        private static readonly Dictionary<string, HornNote> Map = new ()
        {
            {$"{Note.E}{Octave.Lowest}", new HornNote(GuildWarsControls.WeaponSkill1, Octave.Low)},
            {$"{Note.F}{Octave.Lowest}", new HornNote(GuildWarsControls.WeaponSkill2, Octave.Low)},
            {$"{Note.G}{Octave.Lowest}", new HornNote(GuildWarsControls.WeaponSkill3, Octave.Low)},
            {$"{Note.A}{Octave.Lowest}", new HornNote(GuildWarsControls.WeaponSkill4, Octave.Low)},
            {$"{Note.B}{Octave.Lowest}", new HornNote(GuildWarsControls.WeaponSkill5, Octave.Low)},
            {$"{Note.C}{Octave.Low}", new HornNote(GuildWarsControls.HealingSkill, Octave.Low)},
            {$"{Note.D}{Octave.Low}", new HornNote(GuildWarsControls.UtilitySkill1, Octave.Low)},
            {$"{Note.E}{Octave.Low}", new HornNote(GuildWarsControls.WeaponSkill1, Octave.Middle)},
            {$"{Note.F}{Octave.Low}", new HornNote(GuildWarsControls.WeaponSkill2, Octave.Middle)},
            {$"{Note.G}{Octave.Low}", new HornNote(GuildWarsControls.WeaponSkill3, Octave.Middle)},
            {$"{Note.A}{Octave.Low}", new HornNote(GuildWarsControls.WeaponSkill4, Octave.Middle)},
            {$"{Note.B}{Octave.Low}", new HornNote(GuildWarsControls.WeaponSkill5, Octave.Middle)},
            {$"{Note.C}{Octave.Middle}", new HornNote(GuildWarsControls.HealingSkill, Octave.Middle)},
            {$"{Note.D}{Octave.Middle}", new HornNote(GuildWarsControls.UtilitySkill1, Octave.Middle)},
            {$"{Note.E}{Octave.Middle}", new HornNote(GuildWarsControls.WeaponSkill1, Octave.High)},
            {$"{Note.F}{Octave.Middle}", new HornNote(GuildWarsControls.WeaponSkill2, Octave.High)},
            {$"{Note.G}{Octave.Middle}", new HornNote(GuildWarsControls.WeaponSkill3, Octave.High)},
            {$"{Note.A}{Octave.Middle}", new HornNote(GuildWarsControls.WeaponSkill4, Octave.High)},
            {$"{Note.B}{Octave.Middle}", new HornNote(GuildWarsControls.WeaponSkill5, Octave.High)},
            {$"{Note.C}{Octave.High}", new HornNote(GuildWarsControls.HealingSkill, Octave.High)},
            {$"{Note.D}{Octave.High}", new HornNote(GuildWarsControls.UtilitySkill1, Octave.High)},
            {$"{Note.E}{Octave.High}", new HornNote(GuildWarsControls.UtilitySkill2, Octave.High)},
        };

        public HornNote(GuildWarsControls key, Octave octave) : base(key, octave)
        {
            /* NOOP */
        }

        public static HornNote From(RealNote note)
        {
            if (note.Note == Note.Z)
                return new HornNote(GuildWarsControls.None, note.Octave);
            return Map[$"{note.Note}{note.Octave}"];
        }
    }
}