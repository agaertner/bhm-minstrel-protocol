using Nekres.Musician.Core.Domain;
using System.Collections.Generic;
using Blish_HUD.Controls.Intern;

namespace Nekres.Musician.Core.Instrument
{
    public class LuteNote : NoteBase
    {
        private static readonly Dictionary<string, LuteNote> Map = new ()
        {
            {$"{Note.C}{Octave.Lowest}", new LuteNote(GuildWarsControls.WeaponSkill1, Octave.Low)},
            {$"{Note.D}{Octave.Lowest}", new LuteNote(GuildWarsControls.WeaponSkill2, Octave.Low)},
            {$"{Note.E}{Octave.Lowest}", new LuteNote(GuildWarsControls.WeaponSkill3, Octave.Low)},
            {$"{Note.F}{Octave.Lowest}", new LuteNote(GuildWarsControls.WeaponSkill4, Octave.Low)},
            {$"{Note.G}{Octave.Lowest}", new LuteNote(GuildWarsControls.WeaponSkill5, Octave.Low)},
            {$"{Note.A}{Octave.Lowest}", new LuteNote(GuildWarsControls.HealingSkill, Octave.Low)},
            {$"{Note.B}{Octave.Lowest}", new LuteNote(GuildWarsControls.UtilitySkill1, Octave.Low)},
            {$"{Note.C}{Octave.Low}", new LuteNote(GuildWarsControls.WeaponSkill1, Octave.Middle)},
            {$"{Note.D}{Octave.Low}", new LuteNote(GuildWarsControls.WeaponSkill2, Octave.Middle)},
            {$"{Note.E}{Octave.Low}", new LuteNote(GuildWarsControls.WeaponSkill3, Octave.Middle)},
            {$"{Note.F}{Octave.Low}", new LuteNote(GuildWarsControls.WeaponSkill4, Octave.Middle)},
            {$"{Note.G}{Octave.Low}", new LuteNote(GuildWarsControls.WeaponSkill5, Octave.Middle)},
            {$"{Note.A}{Octave.Low}", new LuteNote(GuildWarsControls.HealingSkill, Octave.Middle)},
            {$"{Note.B}{Octave.Low}", new LuteNote(GuildWarsControls.UtilitySkill1, Octave.Middle)},
            {$"{Note.C}{Octave.Middle}", new LuteNote(GuildWarsControls.WeaponSkill1, Octave.High)},
            {$"{Note.D}{Octave.Middle}", new LuteNote(GuildWarsControls.WeaponSkill2, Octave.High)},
            {$"{Note.E}{Octave.Middle}", new LuteNote(GuildWarsControls.WeaponSkill3, Octave.High)},
            {$"{Note.F}{Octave.Middle}", new LuteNote(GuildWarsControls.WeaponSkill4, Octave.High)},
            {$"{Note.G}{Octave.Middle}", new LuteNote(GuildWarsControls.WeaponSkill5, Octave.High)},
            {$"{Note.A}{Octave.Middle}", new LuteNote(GuildWarsControls.HealingSkill, Octave.High)},
            {$"{Note.B}{Octave.Middle}", new LuteNote(GuildWarsControls.UtilitySkill1, Octave.High)},
            {$"{Note.C}{Octave.High}", new LuteNote(GuildWarsControls.UtilitySkill2, Octave.High)}
        };

        public LuteNote(GuildWarsControls key, Octave octave) : base(key, octave)
        {
            /* NOOP */
        }

        public static LuteNote From(RealNote note)
        {
            if (note.Note == Note.Z)
                return new LuteNote(GuildWarsControls.None, note.Octave);
            return Map[$"{note.Note}{note.Octave}"];
        }
    }
}