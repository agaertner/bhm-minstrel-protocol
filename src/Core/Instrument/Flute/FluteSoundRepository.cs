using Blish_HUD.Controls.Intern;
using Microsoft.Xna.Framework.Audio;
using Nekres.Musician.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekres.Musician.Core.Instrument
{
    public class FluteSoundRepository : ISoundRepository
    {
        private readonly Dictionary<string, string> _map = new()
        {
            // Low Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Low}", "E4"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Low}", "F4"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Low}", "G4"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Low}", "A4"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Low}", "B4"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Low}", "C5"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Low}", "D5"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Low}", "E5"},
            //{$"{GuildWarsControls.UtilitySkill3}{FluteNote.Octaves.Low}", "Increase Octave"},
            //{$"{GuildWarsControls.EliteSkill}{FluteNote.Octaves.Low}", "Stop Playing"},

            // High Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.High}", "E5"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.High}", "F5"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.High}", "G5"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.High}", "A5"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.High}", "B5"},
            {$"{GuildWarsControls.HealingSkill}{Octave.High}", "C6"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.High}", "D6"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.High}", "E6"}
            //{$"{GuildWarsControls.UtilitySkill3}{FluteNote.Octaves.Low}", "Decrease Octave"},
            //{$"{GuildWarsControls.EliteSkill}{FluteNote.Octaves.Low}", "Stop Playing"},
        };

        private Dictionary<string, SoundEffectInstance> _sound;

        public SoundEffectInstance Get(GuildWarsControls key, Octave octave)
        {
            return _sound[_map[$"{key}{octave}"]];
        }

        public void Dispose() {
            if (_sound == null) return;
            foreach (var snd in _sound)
                snd.Value?.Dispose();
        }

        public async Task<ISoundRepository> Initialize()
        {
            return await Task.Run(() =>
            {
                _sound ??= new Dictionary<string, SoundEffectInstance>
                {
                    {
                        "E4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\E4.wav")
                            .CreateInstance()
                    },
                    {
                        "F4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\F4.wav")
                            .CreateInstance()
                    },
                    {
                        "G4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\G4.wav")
                            .CreateInstance()
                    },
                    {
                        "A4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\A4.wav")
                            .CreateInstance()
                    },
                    {
                        "B4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\B4.wav")
                            .CreateInstance()
                    },
                    {
                        "C5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\C5.wav")
                            .CreateInstance()
                    },
                    {
                        "D5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\D5.wav")
                            .CreateInstance()
                    },
                    {
                        "E5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\E5.wav")
                            .CreateInstance()
                    },
                    {
                        "F5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\F5.wav")
                            .CreateInstance()
                    },
                    {
                        "G5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\G5.wav")
                            .CreateInstance()
                    },
                    {
                        "A5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\A5.wav")
                            .CreateInstance()
                    },
                    {
                        "B5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\B5.wav")
                            .CreateInstance()
                    },
                    {
                        "C6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\C6.wav")
                            .CreateInstance()
                    },
                    {
                        "D6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\D6.wav")
                            .CreateInstance()
                    },
                    {
                        "E6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Flute\E6.wav")
                            .CreateInstance()
                    }
                };
                return this;
            });
        }
    }
}