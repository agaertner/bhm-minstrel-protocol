using Blish_HUD.Controls;
using Blish_HUD.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Nekres.Musician.UI.Controls
{
    internal class ClipboardButton : Image
    {
        private Texture2D _clipboard;
        private Texture2D _clipboardHover;

        public ClipboardButton()
        {
            _clipboard = MusicianModule.ModuleInstance.ContentsManager.GetTexture("clipboard_hover.png");
            _clipboardHover = MusicianModule.ModuleInstance.ContentsManager.GetTexture("clipboard.png");
            this.Texture = _clipboard;
        }
        protected override void OnMouseEntered(MouseEventArgs e)
        {
            this.Texture = _clipboardHover;
            base.OnMouseMoved(e);
        }

        protected override void OnMouseLeft(MouseEventArgs e)
        {
            this.Texture = _clipboard;
            base.OnMouseLeft(e);
        }

        protected override void DisposeControl()
        {
            _clipboard.Dispose();
            _clipboardHover.Dispose();
            base.DisposeControl();
        }
    }
}
