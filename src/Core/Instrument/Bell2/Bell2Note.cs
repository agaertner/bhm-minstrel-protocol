using System.Collections.Generic;
using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;

namespace Nekres.Musician.Core.Instrument
{
    public class Bell2Note : NoteBase
    {
        private static readonly Dictionary<string, Bell2Note> Map = new ()
        {
            {$"{Note.C}{Octave.Middle}", new Bell2Note(GuildWarsControls.WeaponSkill1, Octave.Low)},
            {$"{Note.D}{Octave.Middle}", new Bell2Note(GuildWarsControls.WeaponSkill2, Octave.Low)},
            {$"{Note.E}{Octave.Middle}", new Bell2Note(GuildWarsControls.WeaponSkill3, Octave.Low)},
            {$"{Note.F}{Octave.Middle}", new Bell2Note(GuildWarsControls.WeaponSkill4, Octave.Low)},
            {$"{Note.G}{Octave.Middle}", new Bell2Note(GuildWarsControls.WeaponSkill5, Octave.Low)},
            {$"{Note.A}{Octave.Middle}", new Bell2Note(GuildWarsControls.HealingSkill, Octave.Low)},
            {$"{Note.B}{Octave.Middle}", new Bell2Note(GuildWarsControls.UtilitySkill1, Octave.Low)},
            {$"{Note.C}{Octave.High}", new Bell2Note(GuildWarsControls.WeaponSkill1, Octave.High)},
            {$"{Note.D}{Octave.High}", new Bell2Note(GuildWarsControls.WeaponSkill2, Octave.High)},
            {$"{Note.E}{Octave.High}", new Bell2Note(GuildWarsControls.WeaponSkill3, Octave.High)},
            {$"{Note.F}{Octave.High}", new Bell2Note(GuildWarsControls.WeaponSkill4, Octave.High)},
            {$"{Note.G}{Octave.High}", new Bell2Note(GuildWarsControls.WeaponSkill5, Octave.High)},
            {$"{Note.A}{Octave.High}", new Bell2Note(GuildWarsControls.HealingSkill, Octave.High)},
            {$"{Note.B}{Octave.High}", new Bell2Note(GuildWarsControls.UtilitySkill1, Octave.High)},
            {$"{Note.C}{Octave.Highest}", new Bell2Note(GuildWarsControls.UtilitySkill2, Octave.High)}
        };

        public Bell2Note(GuildWarsControls key, Octave octave) : base(key, octave)
        {
            /* NOOP */
        }

        public static Bell2Note From(RealNote note)
        {
            if (note.Note == Note.Z)
                return new Bell2Note(GuildWarsControls.None, note.Octave);
            return Map[$"{note.Note}{note.Octave}"];
        }
    }
}