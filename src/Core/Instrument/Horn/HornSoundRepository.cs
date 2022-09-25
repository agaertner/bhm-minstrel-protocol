using System.Collections.Generic;
using System.Threading.Tasks;
using Blish_HUD.Controls.Intern;
using Microsoft.Xna.Framework.Audio;
using Nekres.Musician.Core.Domain;

namespace Nekres.Musician.Core.Instrument
{
    public class HornSoundRepository : ISoundRepository
    {
        private readonly Dictionary<string, string> _map = new()
        {
            // Low Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Low}", "E3"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Low}", "F3"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Low}", "G3"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Low}", "A3"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Low}", "B3"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Low}", "C4"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Low}", "D4"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Low}", "E4"},
            // Middle Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Middle}", "E4"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Middle}", "F4"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Middle}", "G4"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Middle}", "A4"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Middle}", "B4"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Middle}", "C5"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Middle}", "D5"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Middle}", "E5"},
            // High Octave
            {$"{GuildWarsControls.WeaponSkill1}{Octave.High}", "E5"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.High}", "F5"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.High}", "G5"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.High}", "A5"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.High}", "B5"},
            {$"{GuildWarsControls.HealingSkill}{Octave.High}", "C6"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.High}", "D6"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.High}", "E6"}
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
                _sound = new Dictionary<string, SoundEffectInstance>
                {
                    { "E3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\E3.wav").CreateInstance() },
                    { "F3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\F3.wav").CreateInstance() },
                    { "G3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\G3.wav").CreateInstance() },
                    { "A3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\A3.wav").CreateInstance() },
                    { "B3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\B3.wav").CreateInstance() },
                    { "C4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\C4.wav").CreateInstance() },
                    { "D4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\D4.wav").CreateInstance() },
                    { "E4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\E4.wav").CreateInstance() },
                    { "F4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\F4.wav").CreateInstance() },
                    { "G4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\G4.wav").CreateInstance() },
                    { "A4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\A4.wav").CreateInstance() },
                    { "B4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\B4.wav").CreateInstance() },
                    { "C5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\C5.wav").CreateInstance() },
                    { "D5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\D5.wav").CreateInstance() },
                    { "E5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\E5.wav").CreateInstance() },
                    { "F5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\F5.wav").CreateInstance() },
                    { "G5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\G5.wav").CreateInstance() },
                    { "A5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\A5.wav").CreateInstance() },
                    { "B5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\B5.wav").CreateInstance() },
                    { "C6", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\C6.wav").CreateInstance() },
                    { "D6", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\D6.wav").CreateInstance() },
                    { "E6", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Horn\E6.wav").CreateInstance() }
                };
                return this;
            });
        }
    }
}