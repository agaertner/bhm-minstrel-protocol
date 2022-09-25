using Blish_HUD.Controls.Intern;
using Microsoft.Xna.Framework.Audio;
using Nekres.Musician.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nekres.Musician.Core.Instrument
{
    public class HarpSoundRepository : ISoundRepository
    {
        private readonly Dictionary<string, string> _map = new()
        {
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Low}", "C3"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Low}", "D3"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Low}", "E3"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Low}", "F3"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Low}", "G3"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Low}", "A3"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Low}", "B3"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Low}", "C4"},
            {$"{GuildWarsControls.WeaponSkill1}{Octave.Middle}", "C4"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.Middle}", "D4"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.Middle}", "E4"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.Middle}", "F4"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.Middle}", "G4"},
            {$"{GuildWarsControls.HealingSkill}{Octave.Middle}", "A4"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.Middle}", "B4"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.Middle}", "C5"},
            {$"{GuildWarsControls.WeaponSkill1}{Octave.High}", "C5"},
            {$"{GuildWarsControls.WeaponSkill2}{Octave.High}", "D5"},
            {$"{GuildWarsControls.WeaponSkill3}{Octave.High}", "E5"},
            {$"{GuildWarsControls.WeaponSkill4}{Octave.High}", "F5"},
            {$"{GuildWarsControls.WeaponSkill5}{Octave.High}", "G5"},
            {$"{GuildWarsControls.HealingSkill}{Octave.High}", "A5"},
            {$"{GuildWarsControls.UtilitySkill1}{Octave.High}", "B5"},
            {$"{GuildWarsControls.UtilitySkill2}{Octave.High}", "C6"}
        };

        private Dictionary<string, SoundEffectInstance> _sound;

        public async Task<ISoundRepository> Initialize()
        {
            return await Task.Run(() =>
            {
                _sound = new Dictionary<string, SoundEffectInstance>
                {
                    { "C3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\C3.wav").CreateInstance() },
                    { "D3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\D3.wav").CreateInstance() },
                    { "E3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\E3.wav").CreateInstance() },
                    { "F3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\F3.wav").CreateInstance() },
                    { "G3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\G3.wav").CreateInstance() },
                    { "A3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\A3.wav").CreateInstance() },
                    { "B3", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\B3.wav").CreateInstance() },
                    { "C4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\C4.wav").CreateInstance() },
                    { "D4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\D4.wav").CreateInstance() },
                    { "E4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\E4.wav").CreateInstance() },
                    { "F4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\F4.wav").CreateInstance() },
                    { "G4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\G4.wav").CreateInstance() },
                    { "A4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\A4.wav").CreateInstance() },
                    { "B4", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\B4.wav").CreateInstance() },
                    { "C5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\C5.wav").CreateInstance() },
                    { "D5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\D5.wav").CreateInstance() },
                    { "E5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\E5.wav").CreateInstance() },
                    { "F5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\F5.wav").CreateInstance() },
                    { "G5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\G5.wav").CreateInstance() },
                    { "A5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\A5.wav").CreateInstance() },
                    { "B5", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\B5.wav").CreateInstance() },
                    { "C6", MusicianModule.ModuleInstance.ContentsManager.GetSound(@"instruments\Harp\C6.wav").CreateInstance() }
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