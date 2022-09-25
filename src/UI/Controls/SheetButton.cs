using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nekres.Musician.Core.Models;
using Nekres.Musician.UI.Models;
using System;

namespace Nekres.Musician.Controls
{
    internal class SheetButton : DetailsButton
    {
        public event EventHandler<EventArgs> OnPracticeClick;
        public event EventHandler<EventArgs> OnEmulateClick;
        public event EventHandler<ValueEventArgs<bool>> OnPreviewClick;
        public event EventHandler<ValueEventArgs<Guid>> OnDelete;

        private const int SHEETBUTTON_WIDTH = 345;
        private const int SHEETBUTTON_HEIGHT = 100;
        private const int USER_WIDTH = 75;
        private const int BOTTOMSECTION_HEIGHT = 35;

        #region Textures

        private static Texture2D _trashCanClosed = MusicianModule.ModuleInstance.ContentsManager.GetTexture("trashcanClosed_icon_64x64.png");
        private static Texture2D _trashCanOpen = MusicianModule.ModuleInstance.ContentsManager.GetTexture("trashcanOpen_icon_64x64.png");
        private static Texture2D _beatManiaSprite = MusicianModule.ModuleInstance.ContentsManager.GetTexture("beatmania.png");
        private static Texture2D _glowBeatManiaSprite = MusicianModule.ModuleInstance.ContentsManager.GetTexture("glow_beatmania.png");
        private static Texture2D _autoplaySprite = MusicianModule.ModuleInstance.ContentsManager.GetTexture("autoplay.png");
        private static Texture2D _glowAutoplaySprite = MusicianModule.ModuleInstance.ContentsManager.GetTexture("glow_autoplay.png");
        private static Texture2D _playSprite = MusicianModule.ModuleInstance.ContentsManager.GetTexture("stop.png");
        private static Texture2D _glowPlaySprite = MusicianModule.ModuleInstance.ContentsManager.GetTexture("glow_stop.png");
        private static Texture2D _stopSprite = MusicianModule.ModuleInstance.ContentsManager.GetTexture("play.png");
        private static Texture2D _glowStopSprite = MusicianModule.ModuleInstance.ContentsManager.GetTexture("glow_play.png");
        private static Texture2D _dividerSprite = GameService.Content.GetTexture("157218");
        private static Texture2D _iconBoxSprite = GameService.Content.GetTexture("controls/detailsbutton/605003");

        #endregion

        public readonly Guid Id;

        private string _artist;
        public string Artist { 
            get => _artist; 
            set => SetProperty(ref _artist, value);
        }

        private string _user;
        public string User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private Instrument _instrument;
        public Instrument Instrument
        {
            get => _instrument;
            set => SetProperty(ref _instrument, value);
        }

        private bool _isPreviewing;

        private Rectangle _practiceButtonBounds;
        private bool _mouseOverPractice;
        private Rectangle _emulateButtonBounds;
        private bool _mouseOverEmulate;
        private Rectangle _previewButtonBounds;
        private bool _mouseOverPreview;
        private Rectangle _deleteButtonBounds;
        private bool _mouseOverDelete;

        public SheetButton(MusicSheetModel sheet)
        {
            Id = sheet.Id;
            Artist = sheet.Artist;
            User = sheet.User;
            Title = sheet.Title;
            Icon = MusicianModule.ModuleInstance.InstrumentIcons[sheet.Instrument];
            Instrument = sheet.Instrument;
            Size = new Point(SHEETBUTTON_WIDTH, SHEETBUTTON_HEIGHT);
        }

        protected override void OnMouseMoved(MouseEventArgs e)
        {
            this.InvalidateMousePosition();
            base.OnMouseMoved(e);
        }

        protected override void OnClick(MouseEventArgs e)
        {
            base.OnClick(e);
            /*if (_mouseOverPractice)
            {
                OnPracticeClick?.Invoke(this, EventArgs.Empty);
                GameService.Content.PlaySoundEffectByName("error");
                ScreenNotification.ShowNotification("Not yet implemented!");
            }
            else */if (_mouseOverEmulate)
            {
                OnEmulateClick?.Invoke(this, EventArgs.Empty);
                GameService.Content.PlaySoundEffectByName("button-click");
            }
            else if (_mouseOverPreview)
            {
                OnPreviewClick?.Invoke(this, new ValueEventArgs<bool>(!MusicianModule.ModuleInstance.MusicPlayer.IsMySongPlaying(this.Id)));
                GameService.Content.PlaySoundEffectByName("button-click");
            } else if (_mouseOverDelete) {
                OnDelete?.Invoke(this, new ValueEventArgs<Guid>(this.Id));
                this.Dispose();
            }
        }

        protected override CaptureType CapturesInput() {
            return CaptureType.Mouse | CaptureType.Filter;
        }

        protected override void OnMoved(MovedEventArgs e)
        {
            this.InvalidateMousePosition();
            base.OnMoved(e);
        }

        private void InvalidateMousePosition()
        {
            var relPos = RelativeMousePosition;
            _mouseOverPractice = _practiceButtonBounds.Contains(relPos);
            _mouseOverEmulate = _emulateButtonBounds.Contains(relPos);
            _mouseOverPreview = _previewButtonBounds.Contains(relPos);
            _mouseOverDelete = _deleteButtonBounds.Contains(relPos);

            if (_mouseOverPractice)
                this.Parent.BasicTooltipText = "Practice mode (Synthesia)";
            else if (_mouseOverEmulate)
                this.Parent.BasicTooltipText = "Emulate keys (Autoplay)";
            else if (_mouseOverPreview)
                this.Parent.BasicTooltipText = "Preview";
            else if (_mouseOverDelete)
                this.Parent.BasicTooltipText = "Delete";
            else
                this.Parent.BasicTooltipText = string.Empty;
        }

        public override void PaintBeforeChildren(SpriteBatch spriteBatch, Rectangle bounds)
        {
            _isPreviewing = MusicianModule.ModuleInstance.MusicPlayer.IsMySongPlaying(this.Id);

            var iconSize = IconSize == DetailsIconSize.Large ? SHEETBUTTON_HEIGHT : SHEETBUTTON_HEIGHT - BOTTOMSECTION_HEIGHT;

            // Draw background
            spriteBatch.DrawOnCtrl(this, ContentService.Textures.Pixel, bounds, Color.Black * 0.25f);

            // Draw bottom section (overlap to make background darker here)
            spriteBatch.DrawOnCtrl(this, ContentService.Textures.Pixel, new Rectangle(0, bounds.Height - BOTTOMSECTION_HEIGHT, bounds.Width - BOTTOMSECTION_HEIGHT, BOTTOMSECTION_HEIGHT), Color.Black * 0.1f);

            // Draw preview icon
            _previewButtonBounds = new Rectangle(SHEETBUTTON_WIDTH - 36, bounds.Height - BOTTOMSECTION_HEIGHT + 1, 32, 32);
            if (_isPreviewing)
                spriteBatch.DrawOnCtrl(this, _playSprite, _previewButtonBounds, Color.White);
            else
                spriteBatch.DrawOnCtrl(this, _stopSprite, _previewButtonBounds, Color.White);

            /*
            // Draw practice button
            _practiceButtonBounds = new Rectangle(_previewButtonBounds.Left - LEFT_PADDING - 32, bounds.Height - BOTTOMSECTION_HEIGHT + 1, 32, 32);
            if (_mouseOverPractice)
                spriteBatch.DrawOnCtrl(this, _glowBeatManiaSprite, _practiceButtonBounds, Color.White);
            else
                spriteBatch.DrawOnCtrl(this, _beatManiaSprite, _practiceButtonBounds, Color.White);
            */

            // Draw emulate button
            _emulateButtonBounds = new Rectangle(_previewButtonBounds.Left - LEFT_PADDING - 32, bounds.Height - BOTTOMSECTION_HEIGHT + 1, 32, 32);
            if (_mouseOverEmulate)
                spriteBatch.DrawOnCtrl(this, _glowAutoplaySprite, _emulateButtonBounds, Color.White);
            else
                spriteBatch.DrawOnCtrl(this, _autoplaySprite, _emulateButtonBounds, Color.White);

            // Draw delete button
            _deleteButtonBounds = new Rectangle(_emulateButtonBounds.Left - LEFT_PADDING - 32, bounds.Height - BOTTOMSECTION_HEIGHT + 1, 32, 32);
            if (_mouseOverDelete)
                spriteBatch.DrawOnCtrl(this, _trashCanOpen, _deleteButtonBounds, Color.White);
            else
                spriteBatch.DrawOnCtrl(this, _trashCanClosed, _deleteButtonBounds, Color.White);

            // Draw bottom section separator
            spriteBatch.DrawOnCtrl(this, _dividerSprite, new Rectangle(0, bounds.Height - 40, bounds.Width, 8), Color.White);

            // Draw instrument icon
            if (Icon != null)
            {
                var iconBounds = new Rectangle((bounds.Height - BOTTOMSECTION_HEIGHT) / 2 - 32, (bounds.Height - 35) / 2 - 32, 64, 64);
                spriteBatch.DrawOnCtrl(this, this.Icon, iconBounds, Color.White);
                // Draw icon box
                spriteBatch.DrawOnCtrl(this, _iconBoxSprite, iconBounds, Color.White);
            }

            // Wrap text
            var track = Title + " - " + Artist;
            var wrappedText = DrawUtil.WrapText(Content.DefaultFont14, track, SHEETBUTTON_WIDTH - 40 - iconSize - 20);
            spriteBatch.DrawStringOnCtrl(this, wrappedText, Content.DefaultFont14, new Rectangle(89, 0, 216, this.Height - BOTTOMSECTION_HEIGHT), Color.White, false, true, 2, HorizontalAlignment.Left, VerticalAlignment.Middle);

            // Draw the user;
            spriteBatch.DrawStringOnCtrl(this, this.User, Content.DefaultFont14, new Rectangle(5, bounds.Height - BOTTOMSECTION_HEIGHT, USER_WIDTH, 35), Color.White, false, false, 0, HorizontalAlignment.Left, VerticalAlignment.Middle);
        }
    }
}