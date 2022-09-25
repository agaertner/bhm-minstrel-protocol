using Blish_HUD;
using Microsoft.Xna.Framework;
using Nekres.Musician.Core.Domain;
using Nekres.Musician.Core.Instrument;
using System.Diagnostics;

namespace Nekres.Musician.Core.Player.Algorithms
{
    public abstract class PlayAlgorithmBase
    {
        protected bool _abort;

        protected readonly Stopwatch _stopwatch;

        public readonly InstrumentBase Instrument;

        public readonly Vector3 CharacterPosition;

        protected PlayAlgorithmBase(InstrumentBase instrument)
        {
            Instrument = instrument;
            _stopwatch = new Stopwatch();
            CharacterPosition = GameService.Gw2Mumble.PlayerCharacter.Position;
        }

        public abstract void Play(Metronome metronomeMark, ChordOffset[] melody);

        public virtual void Dispose()
        {
            _abort = true;
            _stopwatch.Stop();
        }

        protected bool CharacterMoved()
        {
            return !CharacterPosition.Equals(GameService.Gw2Mumble.PlayerCharacter.Position) && !this.Instrument.Walkable;
        }

        protected bool CanContinue()
        {
            return GameService.Gw2Mumble.IsAvailable
                   && !GameService.Gw2Mumble.UI.IsTextInputFocused
                   && !CharacterMoved();
        }
    }
}