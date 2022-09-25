using Blish_HUD.Controls.Intern;
using System.Collections.Generic;
using Nekres.Musician.Core.Domain;

namespace Nekres.Musician.Core.Instrument
{
    public class HarpNote : NoteBase
    {
        private static readonly Dictionary<string, HarpNote> Map = new ()
        {
            {$"{Note.C}{Octave.Lowest}", new HarpNote(GuildWarsControls.WeaponSkill1, Octave.Low)},
            {$"{Note.D}{Octave.Lowest}", new HarpNote(GuildWarsControls.WeaponSkill2, Octave.Low)},
            {$"{Note.E}{Octave.Lowest}", new HarpNote(GuildWarsControls.WeaponSkill3, Octave.Low)},
            {$"{Note.F}{Octave.Lowest}", new HarpNote(GuildWarsControls.WeaponSkill4, Octave.Low)},
            {$"{Note.G}{Octave.Lowest}", new HarpNote(GuildWarsControls.WeaponSkill5, Octave.Low)},
            {$"{Note.A}{Octave.Lowest}", new HarpNote(GuildWarsControls.HealingSkill, Octave.Low)},
            {$"{Note.B}{Octave.Lowest}", new HarpNote(GuildWarsControls.UtilitySkill1, Octave.Low)},
            {$"{Note.C}{Octave.Low}", new HarpNote(GuildWarsControls.WeaponSkill1, Octave.Middle)},
            {$"{Note.D}{Octave.Low}", new HarpNote(GuildWarsControls.WeaponSkill2, Octave.Middle)},
            {$"{Note.E}{Octave.Low}", new HarpNote(GuildWarsControls.WeaponSkill3, Octave.Middle)},
            {$"{Note.F}{Octave.Low}", new HarpNote(GuildWarsControls.WeaponSkill4, Octave.Middle)},
            {$"{Note.G}{Octave.Low}", new HarpNote(GuildWarsControls.WeaponSkill5, Octave.Middle)},
            {$"{Note.A}{Octave.Low}", new HarpNote(GuildWarsControls.HealingSkill, Octave.Middle)},
            {$"{Note.B}{Octave.Low}", new HarpNote(GuildWarsControls.UtilitySkill1, Octave.Middle)},
            {$"{Note.C}{Octave.Middle}", new HarpNote(GuildWarsControls.WeaponSkill1, Octave.High)},
            {$"{Note.D}{Octave.Middle}", new HarpNote(GuildWarsControls.WeaponSkill2, Octave.High)},
            {$"{Note.E}{Octave.Middle}", new HarpNote(GuildWarsControls.WeaponSkill3, Octave.High)},
            {$"{Note.F}{Octave.Middle}", new HarpNote(GuildWarsControls.WeaponSkill4, Octave.High)},
            {$"{Note.G}{Octave.Middle}", new HarpNote(GuildWarsControls.WeaponSkill5, Octave.High)},
            {$"{Note.A}{Octave.Middle}", new HarpNote(GuildWarsControls.HealingSkill, Octave.High)},
            {$"{Note.B}{Octave.Middle}", new HarpNote(GuildWarsControls.UtilitySkill1, Octave.High)},
            {$"{Note.C}{Octave.High}", new HarpNote(GuildWarsControls.UtilitySkill2, Octave.High)}
        };

        public HarpNote(GuildWarsControls key, Octave octave) : base(key, octave)
        {
            /* NOOP */
        }

        public static HarpNote From(RealNote note)
        {
            if (note.Note == Note.Z)
                return new HarpNote(GuildWarsControls.None, note.Octave);
            return Map[$"{note.Note}{note.Octave}"];
        }
    }
}