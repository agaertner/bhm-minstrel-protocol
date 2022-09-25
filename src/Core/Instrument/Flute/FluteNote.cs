using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;
using System.Collections.Generic;

namespace Nekres.Musician.Core.Instrument
{
    public class FluteNote : NoteBase
    {
        private static readonly Dictionary<string, FluteNote> Map = new()
        {
            // Low Octave
            {$"{Note.E}{Octave.Low}", new FluteNote(GuildWarsControls.WeaponSkill1, Octave.Low)},
            {$"{Note.F}{Octave.Low}", new FluteNote(GuildWarsControls.WeaponSkill2, Octave.Low)},
            {$"{Note.G}{Octave.Low}", new FluteNote(GuildWarsControls.WeaponSkill3, Octave.Low)},
            {$"{Note.A}{Octave.Low}", new FluteNote(GuildWarsControls.WeaponSkill4, Octave.Low)},
            {$"{Note.B}{Octave.Low}", new FluteNote(GuildWarsControls.WeaponSkill5, Octave.Low)},
            {$"{Note.C}{Octave.Middle}", new FluteNote(GuildWarsControls.HealingSkill, Octave.Low)},
            {$"{Note.D}{Octave.Middle}", new FluteNote(GuildWarsControls.UtilitySkill1, Octave.Low)},
            {$"{Note.E}{Octave.Middle}", new FluteNote(GuildWarsControls.UtilitySkill2, Octave.Low)},

            // High Octave
            {$"{Note.F}{Octave.Middle}", new FluteNote(GuildWarsControls.WeaponSkill2, Octave.High)},
            {$"{Note.G}{Octave.Middle}", new FluteNote(GuildWarsControls.WeaponSkill3, Octave.High)},
            {$"{Note.A}{Octave.Middle}", new FluteNote(GuildWarsControls.WeaponSkill4, Octave.High)},
            {$"{Note.B}{Octave.Middle}", new FluteNote(GuildWarsControls.WeaponSkill5, Octave.High)},

            // Highest Octave
            {$"{Note.C}{Octave.High}", new FluteNote(GuildWarsControls.HealingSkill, Octave.High)},
            {$"{Note.D}{Octave.High}", new FluteNote(GuildWarsControls.UtilitySkill1, Octave.High)},
            {$"{Note.E}{Octave.High}", new FluteNote(GuildWarsControls.UtilitySkill2, Octave.High)}
        };

        public FluteNote(GuildWarsControls key, Octave octave) : base(key, octave)
        {
            /* NOOP */
        }

        public static FluteNote From(RealNote note)
        {
            if (note.Note == Note.Z) 
                return new FluteNote(GuildWarsControls.None, note.Octave);
            return Map[$"{note.Note}{note.Octave}"];
        }
    }
}