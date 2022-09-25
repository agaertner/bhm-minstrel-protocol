using Blish_HUD.Controls.Intern;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nekres.Musician.Core.Domain;

namespace Nekres.Musician.Core.Instrument
{
    public class Bell2SoundRepository : ISoundRepository
    {
        private readonly Dictionary<string, string> _map = new()
        {
            // Low Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Low}", "C5"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Low}", "D5"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Low}", "E5"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Low}", "F5"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Low}", "G5"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Low}", "A5"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Low}", "B5"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Low}", "C6"},
            // High Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.High}", "C6"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.High}", "D6"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.High}", "E6"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.High}", "F6"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.High}", "G6"},
            {$"{GuildWarsControls.HealingSkill}{Octave.High}", "A6"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.High}", "B6"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.High}", "C7"}
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
                        "C5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\C5.wav")
                            .CreateInstance()
                    },
                    {
                        "D5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\D5.wav")
                            .CreateInstance()
                    },
                    {
                        "E5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\E5.wav")
                            .CreateInstance()
                    },
                    {
                        "F5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\F5.wav")
                            .CreateInstance()
                    },
                    {
                        "G5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\G5.wav")
                            .CreateInstance()
                    },
                    {
                        "A5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\A5.wav")
                            .CreateInstance()
                    },
                    {
                        "B5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\B5.wav")
                            .CreateInstance()
                    },
                    {
                        "C6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\C6.wav")
                            .CreateInstance()
                    },
                    {
                        "D6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\D6.wav")
                            .CreateInstance()
                    },
                    {
                        "E6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\E6.wav")
                            .CreateInstance()
                    },
                    {
                        "F6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\F6.wav")
                            .CreateInstance()
                    },
                    {
                        "G6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\G6.wav")
                            .CreateInstance()
                    },
                    {
                        "A6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\A6.wav")
                            .CreateInstance()
                    },
                    {
                        "B6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\B6.wav")
                            .CreateInstance()
                    },
                    {
                        "C7",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell2\C7.wav")
                            .CreateInstance()
                    }
                };
                return this;
            });
        }
    }
}