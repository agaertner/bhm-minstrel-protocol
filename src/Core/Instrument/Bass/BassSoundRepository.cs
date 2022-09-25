using Blish_HUD.Controls.Intern;
using Microsoft.Xna.Framework.Audio;
using Nekres.Musician.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekres.Musician.Core.Instrument
{
    public class BassSoundRepository : ISoundRepository
    {
        private readonly Dictionary<string, string> _map = new()
        {
            // Low Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Low}", "C1"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Low}", "D1"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Low}", "E1"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Low}", "F1"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Low}", "G1"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Low}", "A1"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Low}", "B1"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Low}", "C2"},
            // High Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.High}", "C2"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.High}", "D2"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.High}", "E2"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.High}", "F2"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.High}", "G2"},
            {$"{GuildWarsControls.HealingSkill}{Octave.High}", "A2"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.High}", "B2"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.High}", "C3"}
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
                        "C1",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\C1.wav")
                            .CreateInstance()
                    },
                    {
                        "D1",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\D1.wav")
                            .CreateInstance()
                    },
                    {
                        "E1",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\E1.wav")
                            .CreateInstance()
                    },
                    {
                        "F1",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\F1.wav")
                            .CreateInstance()
                    },
                    {
                        "G1",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\G1.wav")
                            .CreateInstance()
                    },
                    {
                        "A1",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\A1.wav")
                            .CreateInstance()
                    },
                    {
                        "B1",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\B1.wav")
                            .CreateInstance()
                    },
                    {
                        "C2",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\C2.wav")
                            .CreateInstance()
                    },
                    {
                        "D2",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\D2.wav")
                            .CreateInstance()
                    },
                    {
                        "E2",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\E2.wav")
                            .CreateInstance()
                    },
                    {
                        "F2",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\F2.wav")
                            .CreateInstance()
                    },
                    {
                        "G2",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\G2.wav")
                            .CreateInstance()
                    },
                    {
                        "A2",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\A2.wav")
                            .CreateInstance()
                    },
                    {
                        "B2",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\B2.wav")
                            .CreateInstance()
                    },
                    {
                        "C3",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bass\C3.wav")
                            .CreateInstance()
                    }
                };
                return this;
            });
        }
    }
}
