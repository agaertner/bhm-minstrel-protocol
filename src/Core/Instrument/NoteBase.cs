using Blish_HUD.Controls.Intern;
using Nekres.Musician.Core.Domain;

namespace Nekres.Musician.Core.Instrument
{
    public abstract class NoteBase
    {
        public readonly GuildWarsControls Key;

        public readonly Octave Octave;

        protected NoteBase(GuildWarsControls key, Octave octave)
        {
            this.Key = key;
            this.Octave = octave;
        }

        public override bool Equals(object obj)
        {
            return Equals((NoteBase)obj);
        }

        protected bool Equals(NoteBase other)
        {
            return Key == other.Key && Octave == other.Octave;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Key * 397) ^ (int)Octave;
            }
        }
    }
}
