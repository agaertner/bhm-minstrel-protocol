using Blish_HUD.Controls.Intern;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nekres.Musician.Core.Domain;

namespace Nekres.Musician.Core.Instrument
{
    public class LuteSoundRepository : ISoundRepository
    {
        private readonly Dictionary<string, string> _map = new()
        {
            // Low Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Low}", "C3"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Low}", "D3"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Low}", "E3"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Low}", "F3"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Low}", "G3"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Low}", "A3"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Low}", "B3"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Low}", "C4"},
            // Middle Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Middle}", "C4"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Middle}", "D4"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Middle}", "E4"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Middle}", "F4"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Middle}", "G4"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Middle}", "A4"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Middle}", "B4"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Middle}", "C5"},
            // High Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.High}", "C5"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.High}", "D5"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.High}", "E5"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.High}", "F5"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.High}", "G5"},
            {$"{GuildWarsControls.HealingSkill}{Octave.High}", "A5"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.High}", "B5"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.High}", "C6"},
        };

        private Dictionary<string, SoundEffectInstance> _sound;

        public async Task<ISoundRepository> Initialize()
        {
            return await Task.Run(() =>
            {
                _sound ??= new Dictionary<string, SoundEffectInstance>
                {
                    {
                        "C3",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\C3.wav")
                            .CreateInstance()
                    },
                    {
                        "D3",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\D3.wav")
                            .CreateInstance()
                    },
                    {
                        "E3",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\E3.wav")
                            .CreateInstance()
                    },
                    {
                        "F3",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\F3.wav")
                            .CreateInstance()
                    },
                    {
                        "G3",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\G3.wav")
                            .CreateInstance()
                    },
                    {
                        "A3",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\A3.wav")
                            .CreateInstance()
                    },
                    {
                        "B3",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\B3.wav")
                            .CreateInstance()
                    },
                    {
                        "C4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\C4.wav")
                            .CreateInstance()
                    },
                    {
                        "D4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\D4.wav")
                            .CreateInstance()
                    },
                    {
                        "E4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\E4.wav")
                            .CreateInstance()
                    },
                    {
                        "F4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\F4.wav")
                            .CreateInstance()
                    },
                    {
                        "G4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\G4.wav")
                            .CreateInstance()
                    },
                    {
                        "A4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\A4.wav")
                            .CreateInstance()
                    },
                    {
                        "B4",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\B4.wav")
                            .CreateInstance()
                    },
                    {
                        "C5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\C5.wav")
                            .CreateInstance()
                    },
                    {
                        "D5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\D5.wav")
                            .CreateInstance()
                    },
                    {
                        "E5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\E5.wav")
                            .CreateInstance()
                    },
                    {
                        "F5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\F5.wav")
                            .CreateInstance()
                    },
                    {
                        "G5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\G5.wav")
                            .CreateInstance()
                    },
                    {
                        "A5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\A5.wav")
                            .CreateInstance()
                    },
                    {
                        "B5",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\B5.wav")
                            .CreateInstance()
                    },
                    {
                        "C6",
                        MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Lute\C6.wav")
                            .CreateInstance()
                    }
                };
                return this;
            });
        }

        public SoundEffectInstance Get(GuildWarsControls key, Octave octave)
        {
            return _sound[_map[$"{key}{octave}"]];
        }

        public void Dispose() {
            if (_sound == null) return;
            foreach (var snd in _sound)
                snd.Value?.Dispose();
        }
    }
}
