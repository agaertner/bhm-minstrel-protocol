using System;
using System.Threading.Tasks;
using Blish_HUD.Controls.Intern;
using Microsoft.Xna.Framework.Audio;
using Nekres.Musician.Core.Domain;

namespace Nekres.Musician.Core.Instrument
{
    public interface ISoundRepository : IDisposable
    {
        Task<ISoundRepository> Initialize();
        SoundEffectInstance Get(GuildWarsControls key, Octave octave);
    }
}
