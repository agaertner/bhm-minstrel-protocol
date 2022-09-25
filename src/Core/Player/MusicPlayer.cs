using Microsoft.Xna.Framework.Audio;
using Nekres.Musician.Core.Instrument;
using Nekres.Musician.Core.Models;
using Nekres.Musician.Core.Player.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;

namespace Nekres.Musician.Core.Player
{
    internal class MusicPlayer : IDisposable
    {
        private readonly Dictionary<Models.Instrument, ISoundRepository> _soundRepositories;

        private PlayAlgorithmBase _algorithm;

        private SoundEffectInstance _activeSfx;

        private Guid _activeMusicSheet;

        private HealthPoolButton _stopButton;

        private float _audioVolume => MusicianModule.ModuleInstance.audioVolume.Value / 1000;

        public MusicPlayer()
        {
            _soundRepositories = new Dictionary<Models.Instrument, ISoundRepository>
            {
                {Models.Instrument.Bass, new BassSoundRepository()},
                {Models.Instrument.Bell, new BellSoundRepository()},
                {Models.Instrument.Bell2, new Bell2SoundRepository()},
                {Models.Instrument.Flute, new FluteSoundRepository()},
                {Models.Instrument.Harp, new HarpSoundRepository()},
                {Models.Instrument.Horn, new HornSoundRepository()},
                {Models.Instrument.Lute, new LuteSoundRepository()}
            };
        }

        public void Dispose()
        {
            foreach (var (_, soundRepo) in _soundRepositories) soundRepo.Dispose();
            this.Stop();
            _stopButton?.Dispose();
        }

        public void PlaySound(SoundEffectInstance sfx, bool loops = false)
        {
            if (loops)
            {
                StopSound();
                sfx.IsLooped = true;
            }
            _activeSfx = sfx;
            sfx.Volume = _audioVolume;
            sfx.Play();
        }

        public void StopSound() => _activeSfx?.Stop();

        public bool IsMySongPlaying(Guid id) => _activeMusicSheet.Equals(id);

        public async Task PlayPreview(MusicSheet musicSheet) => Play(musicSheet, await GetInstrumentPreview(musicSheet.Instrument));

        public void PlayEmulate(MusicSheet musicSheet) => Play(musicSheet, GetInstrumentEmulate(musicSheet.Instrument));

        private void Play(MusicSheet musicSheet, InstrumentBase instrument)
        {
            this.Stop();
            _algorithm = musicSheet.Algorithm == Algorithm.FavorChords ? new FavorChordsAlgorithm(instrument) : new FavorNotesAlgorithm(instrument);
            var worker = new Thread(() => _algorithm?.Play(musicSheet.Tempo, musicSheet.Melody.ToArray()));
            worker.Start();
            _activeMusicSheet = musicSheet.Id;
            _stopButton = new HealthPoolButton
            {
                Parent = GameService.Graphics.SpriteScreen,
                Text = "Click to Stop"
            };
            _stopButton.Click += ClickStopButton;
        }

        private void ClickStopButton(object o, MouseEventArgs e) => this.Stop();

        public void Stop()
        {
            this.StopSound();
            _activeMusicSheet = Guid.Empty;
            _algorithm?.Dispose();
            _algorithm = null;
            _stopButton?.Dispose();
        }

        private InstrumentBase GetInstrumentEmulate(Models.Instrument instrument)
        {
            switch (instrument)
            {
                case Models.Instrument.Bass:
                    return new Bass();
                case Models.Instrument.Bell:
                    return new Bell();
                case Models.Instrument.Bell2:
                    return new Bell2();
                case Models.Instrument.Flute:
                    return new Flute();
                case Models.Instrument.Harp:
                    return new Harp();
                case Models.Instrument.Horn:
                    return new Horn();
                case Models.Instrument.Lute:
                    return new Lute();
                default: break;
            }
            return null;
        }

        private async Task<InstrumentBase> GetInstrumentPreview(Models.Instrument instrument)
        {

            switch (instrument)
            {
                case Models.Instrument.Bass:
                    return new BassPreview(await _soundRepositories[instrument].Initialize());
                case Models.Instrument.Bell:
                    return new BellPreview(await _soundRepositories[instrument].Initialize());
                case Models.Instrument.Bell2:
                    return new Bell2Preview(await _soundRepositories[instrument].Initialize());
                case Models.Instrument.Flute:
                    return new FlutePreview(await _soundRepositories[instrument].Initialize());
                case Models.Instrument.Harp:
                    return new HarpPreview(await _soundRepositories[instrument].Initialize());
                case Models.Instrument.Horn:
                    return new HornPreview(await _soundRepositories[instrument].Initialize());
                case Models.Instrument.Lute:
                    return new LutePreview(await _soundRepositories[instrument].Initialize());
                default: break;
            }
            return null;
        }
    }
}