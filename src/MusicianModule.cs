using Blish_HUD;
using Blish_HUD.Content;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Blish_HUD.Modules;
using Blish_HUD.Modules.Managers;
using Blish_HUD.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nekres.Musician.Core.Models;
using Nekres.Musician.Core.Player;
using Nekres.Musician.UI;
using Nekres.Musician.UI.Models;
using Nekres.Musician.UI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace Nekres.Musician
{

    [Export(typeof(Module))]
    public class MusicianModule : Module
    {
        internal static readonly Logger Logger = Logger.GetLogger(typeof(MusicianModule));

        internal static MusicianModule ModuleInstance;

        #region Service Managers
        internal SettingsManager SettingsManager => this.ModuleParameters.SettingsManager;
        internal ContentsManager ContentsManager => this.ModuleParameters.ContentsManager;
        internal DirectoriesManager DirectoriesManager => this.ModuleParameters.DirectoriesManager;
        internal Gw2ApiManager Gw2ApiManager => this.ModuleParameters.Gw2ApiManager;
        #endregion

        #region Settings

        internal SettingEntry<float> audioVolume;
        internal SettingEntry<KeyBinding> keySwapWeapons;
        internal SettingEntry<KeyBinding> keyWeaponSkill1;
        internal SettingEntry<KeyBinding> keyWeaponSkill2;
        internal SettingEntry<KeyBinding> keyWeaponSkill3;
        internal SettingEntry<KeyBinding> keyWeaponSkill4;
        internal SettingEntry<KeyBinding> keyWeaponSkill5;
        internal SettingEntry<KeyBinding> keyHealingSkill;
        internal SettingEntry<KeyBinding> keyUtilitySkill1;
        internal SettingEntry<KeyBinding> keyUtilitySkill2;
        internal SettingEntry<KeyBinding> keyUtilitySkill3;
        internal SettingEntry<KeyBinding> keyEliteSkill;

        // Self Managed
        internal SettingEntry<string> SheetFilter;
        internal SettingEntry<bool> DefaultSheetsImported;
        internal SettingEntry<float> OctaveOffsetDelay;
        #endregion

        private CornerIcon _moduleIcon;
        private Texture2D _moduleIconTex;
        
        private StandardWindow _moduleWindow;
        private Texture2D _moduleWindowBackground;
        private Texture2D _moduleEmblem;

        internal MusicPlayer MusicPlayer { get; private set; }

        internal MusicSheetService MusicSheetService { get; private set; }

        internal MusicSheetImporter MusicSheetImporter { get; private set; }

        internal Dictionary<Instrument, AsyncTexture2D> InstrumentIcons;

        [ImportingConstructor]
        public MusicianModule([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters) { ModuleInstance = this; }

        protected override void DefineSettings(SettingCollection settingsManager)
        {
            audioVolume = settingsManager.DefineSetting("audioVolume", 80f, 
                () => "Audio Volume", 
                () => "Audio volume of this module.");
            OctaveOffsetDelay = settingsManager.DefineSetting("octaveOffsetDelayMs", 100f, 
                () => "Speed", 
                () => "The speed at which instruments are played.\nLeft: slower. Right: faster.\nMin: One second delay. Max: normal speed.");

            var skillKeyBindingsCollection = settingsManager.AddSubCollection("Skills", true, false);
            keySwapWeapons = skillKeyBindingsCollection.DefineSetting("keySwapWeapons", new KeyBinding(Keys.OemPipe), () => "Swap Weapons");
            keyWeaponSkill1 = skillKeyBindingsCollection.DefineSetting("keyWeaponSkill1", new KeyBinding(Keys.D1), () => "Weapon Skill 1");
            keyWeaponSkill2 = skillKeyBindingsCollection.DefineSetting("keyWeaponSkill2", new KeyBinding(Keys.D2), () => "Weapon Skill 2");
            keyWeaponSkill3 = skillKeyBindingsCollection.DefineSetting("keyWeaponSkill3", new KeyBinding(Keys.D3), () => "Weapon Skill 3");
            keyWeaponSkill4 = skillKeyBindingsCollection.DefineSetting("keyWeaponSkill4", new KeyBinding(Keys.D4), () => "Weapon Skill 4");
            keyWeaponSkill5 = skillKeyBindingsCollection.DefineSetting("keyWeaponSkill5", new KeyBinding(Keys.D5), () => "Weapon Skill 5");
            keyHealingSkill = skillKeyBindingsCollection.DefineSetting("keyHealingSkill", new KeyBinding(Keys.D6), () => "Healing Skill");
            keyUtilitySkill1 = skillKeyBindingsCollection.DefineSetting("keyUtilitySkill1", new KeyBinding(Keys.D7), () => "Utility Skill 1");
            keyUtilitySkill2 = skillKeyBindingsCollection.DefineSetting("keyUtilitySkill2", new KeyBinding(Keys.D8), () => "Utility Skill 2");
            keyUtilitySkill3 = skillKeyBindingsCollection.DefineSetting("keyUtilitySkill3", new KeyBinding(Keys.D9), () => "Utility Skill 3");
            keyEliteSkill = skillKeyBindingsCollection.DefineSetting("keyEliteSkill", new KeyBinding(Keys.D0), () => "Elite Skill");


            var selfManagedSettings = settingsManager.AddSubCollection("selfManaged", false, false);
            SheetFilter = selfManagedSettings.DefineSetting("sheetFilter", "Title");
            DefaultSheetsImported = selfManagedSettings.DefineSetting("defaultSheetsImported", false);
            //OctaveOffsetDelay = selfManagedSettings.DefineSetting("octaveOffsetDelayMs", 0);
            GameService.GameIntegration.Gw2Instance.IsInGameChanged += OnIsInGameChanged;
        }

        private void OnIsInGameChanged(object o, ValueEventArgs<bool> e)
        {
            if (e.Value)
            {
                CreateModuleIcon();
                return;
            }

            DisposeModuleIcon();
            _moduleWindow?.Hide();
        }

        protected override void Initialize()
        {
            _moduleIconTex = ContentsManager.GetTexture("corner_icon.png");
            _moduleWindowBackground = ContentsManager.GetTexture("background.png");
            _moduleEmblem = ContentsManager.GetTexture("musician_icon.png");

            MusicSheetService = new MusicSheetService(DirectoriesManager.GetFullDirectoryPath("musician"));
            MusicPlayer = new MusicPlayer();

            InstrumentIcons = new Dictionary<Instrument, AsyncTexture2D>();
            foreach (var instrument in Enum.GetValues(typeof(Instrument)).Cast<Instrument>())
                InstrumentIcons.Add(instrument, new AsyncTexture2D());
        }

        protected override async Task LoadAsync()
        {
            MusicSheetService.LoadDatabase();
            this.MusicSheetImporter = new MusicSheetImporter(this.MusicSheetService, GetModuleProgressHandler());

            // Load icons
            await Task.Run(() =>
            {
                foreach (var (instrument, icon) in InstrumentIcons)
                    icon.SwapTexture(ContentsManager.GetTexture($@"instruments\{instrument.ToString().ToLowerInvariant()}.png"));
            });

            if (DefaultSheetsImported.Value) return;
            var sheets = new[]
            {
                "Age of Empires 2 - Main Theme.xml",
                "Alan Menken - A Whole New World.xml",
                "Fall Out Boy - Centuries.xml",
                "Family Guy - Stewie Follows a Fat Guy With a Tuba.xml",
                "Howard Shore - Concerning Hobbits.xml",
                "Jeremy Soule - Fear Not This Night.xml",
                "Jeremy Soule - The Tengu Wall.xml",
                "Joseph Mohr and Franz Xaver Gruber - Silent Night.xml",
                "Junichi Masuda - Jigglypuff's Song.xml",
                "Junichi Masuda - Pokémon Center Theme.xml",
                "Kirby - Gourmet Race.xml",
                "Kris Wu Yi Fan - Time Boils the Rain.xml",
                "Maclaine Diemer - Bash the Dragon.xml",
                "Masayoshi Minoshima - Bad Apple.xml",
                "Michael Jackson - Smooth Criminal.xml",
                "Mulan - I'll Make A Man Out Of You.xml",
                "Phoenix Legend - Most Dazzling Folk Style.xml",
                "Tetris - Main Theme.xml",
                "The Legend of Zelda - Gerudo Valley.xml",
                "The Legend of Zelda - Kakariko Village.xml",
                "The Legend of Zelda - Lost Woods.xml",
                "The Legend of Zelda - Song of Healing.xml",
                "The Legend of Zelda - Song of Time.xml",
                "The Legend of Zelda - Zelda's Lullaby.xml",
                "Westworld - Main Theme.xml",
                "Yiruma - River Flows in You.xml"
            };
            await Task.Run(async () =>
            {
                foreach (var sheet in sheets)
                    await MusicSheetImporter.ImportFromStream(ContentsManager.GetFileStream($@"notesheets\{sheet}"), true);
            });
            DefaultSheetsImported.Value = true;
        }

        private void UpdateModuleLoading(string loadingMessage)
        {
            if (_moduleIcon == null) return;
            _moduleIcon.LoadingMessage = loadingMessage;
        }

        public IProgress<string> GetModuleProgressHandler()
        {
            return new Progress<string>(UpdateModuleLoading);
        }

        protected override void OnModuleLoaded(EventArgs e)
        {
            if (GameService.GameIntegration.Gw2Instance.IsInGame)
            {
                CreateModuleIcon();
            }

            CreateModuleWindow();

            MusicSheetImporter.Init();
            base.OnModuleLoaded(e);
        }

        private void CreateModuleIcon()
        {
            if (_moduleIcon != null) return;
            _moduleIcon = new CornerIcon(_moduleIconTex, this.Name);
            _moduleIcon.Click += OnModuleIconClick;
        }

        private void DisposeModuleIcon()
        {
            if (_moduleIcon == null) return;
            _moduleIcon.Click -= OnModuleIconClick;
            _moduleIcon.Dispose();
            _moduleIcon = null;
        }

        private void CreateModuleWindow()
        {
            var windowRegion = new Rectangle(40, 26, 423, 780 - 56);
            var contentRegion = new Rectangle(70, 41, 380, 780 - 42);
            _moduleWindow = new StandardWindow(_moduleWindowBackground, windowRegion, contentRegion)
            {
                Parent = GameService.Graphics.SpriteScreen,
                Emblem = _moduleEmblem,
                Location = new Point((GameService.Graphics.SpriteScreen.Width - windowRegion.Width) / 2, (GameService.Graphics.SpriteScreen.Height - windowRegion.Height) / 2),
                SavesPosition = true,
                Id = $"{this.Name}_Library_2a961a74-4214-4ea1-8e3f-4897b301ced9",
                Title = this.Name
            };
        }

        private void OnModuleIconClick(object o, MouseEventArgs e)
        {
            _moduleWindow.ToggleWindow(new LibraryView(new LibraryModel(MusicSheetService)));
        }

        protected override void Unload()
        {
            GameService.GameIntegration.Gw2Instance.IsInGameChanged -= OnIsInGameChanged;
            DisposeModuleIcon();
            _moduleWindow?.Dispose();
            _moduleEmblem?.Dispose();
            _moduleWindowBackground?.Dispose();
            _moduleIconTex?.Dispose();
            MusicPlayer?.Dispose();
            MusicSheetService?.Dispose();
            MusicSheetImporter?.Dispose();
            ModuleInstance = null;
        }
    }
}
