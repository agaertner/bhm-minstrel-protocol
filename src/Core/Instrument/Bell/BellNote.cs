using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using System.Collections.Generic;

namespace Nekres.Musician.Core.Instrument
{
    public class BellNote : NoteBase
    {
        private static readonly Dictionary<string, BellNote> Map = new()
        {
            {$"{Note.D}{Octave.Low}", new BellNote(GuildWarsControls.WeaponSkill1, Octave.Low)},
            {$"{Note.E}{Octave.Low}", new BellNote(GuildWarsControls.WeaponSkill2, Octave.Low)},
            {$"{Note.F}{Octave.Low}", new BellNote(GuildWarsControls.WeaponSkill3, Octave.Low)},
            {$"{Note.G}{Octave.Low}", new BellNote(GuildWarsControls.WeaponSkill4, Octave.Low)},
            {$"{Note.A}{Octave.Low}", new BellNote(GuildWarsControls.WeaponSkill5, Octave.Low)},
            {$"{Note.B}{Octave.Low}", new BellNote(GuildWarsControls.HealingSkill, Octave.Low)},
            {$"{Note.C}{Octave.Middle}", new BellNote(GuildWarsControls.UtilitySkill1, Octave.Low)},
            {$"{Note.D}{Octave.Middle}", new BellNote(GuildWarsControls.WeaponSkill1, Octave.Middle)},
            {$"{Note.E}{Octave.Middle}", new BellNote(GuildWarsControls.WeaponSkill2, Octave.Middle)},
            {$"{Note.F}{Octave.Middle}", new BellNote(GuildWarsControls.WeaponSkill3, Octave.Middle)},
            {$"{Note.G}{Octave.Middle}", new BellNote(GuildWarsControls.WeaponSkill4, Octave.Middle)},
            {$"{Note.A}{Octave.Middle}", new BellNote(GuildWarsControls.WeaponSkill5, Octave.Middle)},
            {$"{Note.B}{Octave.Middle}", new BellNote(GuildWarsControls.HealingSkill, Octave.Middle)},
            {$"{Note.C}{Octave.High}", new BellNote(GuildWarsControls.UtilitySkill1, Octave.Middle)},
            {$"{Note.D}{Octave.High}", new BellNote(GuildWarsControls.WeaponSkill1, Octave.High)},
            {$"{Note.E}{Octave.High}", new BellNote(GuildWarsControls.WeaponSkill2, Octave.High)},
            {$"{Note.F}{Octave.High}", new BellNote(GuildWarsControls.WeaponSkill3, Octave.High)},
            {$"{Note.G}{Octave.High}", new BellNote(GuildWarsControls.WeaponSkill4, Octave.High)},
            {$"{Note.A}{Octave.High}", new BellNote(GuildWarsControls.WeaponSkill5, Octave.High)},
            {$"{Note.B}{Octave.High}", new BellNote(GuildWarsControls.HealingSkill, Octave.High)},
            {$"{Note.C}{Octave.Highest}", new BellNote(GuildWarsControls.UtilitySkill1, Octave.High)},
            {$"{Note.D}{Octave.Highest}", new BellNote(GuildWarsControls.UtilitySkill2, Octave.High)},
        };

        public BellNote(GuildWarsControls key, Octave octave) : base(key, octave)
        {
            /* NOOP */
        }

        public static BellNote From(RealNote note)
        {
            if (note.Note == Note.Z)
                return new BellNote(GuildWarsControls.None, note.Octave);
            return Map[$"{note.Note}{note.Octave}"];
        }
    }
}