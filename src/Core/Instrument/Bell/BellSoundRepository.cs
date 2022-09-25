using Blish_HUD.Controls.Intern;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nekres.Musician.Core.Domain;

namespace Nekres.Musician.Core.Instrument
{
    public class BellSoundRepository : ISoundRepository
    {
        private readonly Dictionary<string, string> _map = new()
        {
            // Low Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Low}", "D4"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Low}", "E4"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Low}", "F4"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Low}", "G4"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Low}", "A4"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Low}", "B4"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Low}", "C5"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Low}", "D5"},
            // Middle Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Middle}", "D5"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Middle}", "E5"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Middle}", "F5"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Middle}", "G5"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Middle}", "A5"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Middle}", "B5"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Middle}", "C6"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Middle}", "D6"},
            // High Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.High}", "D6"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.High}", "E6"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.High}", "F6"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.High}", "G6"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.High}", "A6"},
            {$"{GuildWarsControls.HealingSkill}{Octave.High}", "B6"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.High}", "C7"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.High}", "D7"}
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
                        "D4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\D4.wav")
                            .CreateInstance()
                    },
                    {
                        "E4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\E4.wav")
                            .CreateInstance()
                    },
                    {
                        "F4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\F4.wav")
                            .CreateInstance()
                    },
                    {
                        "G4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\G4.wav")
                            .CreateInstance()
                    },
                    {
                        "A4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\A4.wav")
                            .CreateInstance()
                    },
                    {
                        "B4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\B4.wav")
                            .CreateInstance()
                    },
                    {
                        "C5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\C5.wav")
                            .CreateInstance()
                    },
                    {
                        "D5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\D5.wav")
                            .CreateInstance()
                    },
                    {
                        "E5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\E5.wav")
                            .CreateInstance()
                    },
                    {
                        "F5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\F5.wav")
                            .CreateInstance()
                    },
                    {
                        "G5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\G5.wav")
                            .CreateInstance()
                    },
                    {
                        "A5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\A5.wav")
                            .CreateInstance()
                    },
                    {
                        "B5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\B5.wav")
                            .CreateInstance()
                    },
                    {
                        "C6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\C6.wav")
                            .CreateInstance()
                    },
                    {
                        "D6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\D6.wav")
                            .CreateInstance()
                    },
                    {
                        "E6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\E6.wav")
                            .CreateInstance()
                    },
                    {
                        "F6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\F6.wav")
                            .CreateInstance()
                    },
                    {
                        "G6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\G6.wav")
                            .CreateInstance()
                    },
                    {
                        "A6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\A6.wav")
                            .CreateInstance()
                    },
                    {
                        "B6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\B6.wav")
                            .CreateInstance()
                    },
                    {
                        "C7",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\C7.wav")
                            .CreateInstance()
                    },
                    {
                        "D7",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Bell\D7.wav")
                            .CreateInstance()
                    }
                };
                return this;
            });
        }
    }
}