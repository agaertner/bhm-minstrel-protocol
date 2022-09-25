using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Nekres.Musician.UI.Controls
{
    internal class IntegerInput : Panel
    {
        public event EventHandler<ValueEventArgs<int>> ValueChanged;
        
        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _suffix;
        public string Suffix
        {
            get => _suffix;
            set => SetProperty(ref _suffix, value);
        }

        private TextBox _inputTextBox;

        private int _value;
        public int Value
        {
            get => _value;
            private set
            {
                SetProperty(ref _value, value);
                ValueChanged?.Invoke(this, new ValueEventArgs<int>(value));
            }
        }

        public IntegerInput(string text, int value, string suffix = "")
        {
            _text = text;
            _value = value;
            _suffix = suffix;
        }

        protected override void DisposeControl()
        {
            _inputTextBox?.Dispose();
            base.DisposeControl();
        }

        private void CreateInputTextBox(Rectangle bounds)
        {
            _inputTextBox ??= new TextBox
            {
                Parent = this,
                Text = _value.ToString(),
                Font = GameService.Content.DefaultFont14,
                Size = new Point(bounds.Width, bounds.Height),
                Location = new Point(bounds.X, bounds.Y)
            };
            var text = _value.ToString();
            _inputTextBox.InputFocusChanged += (_, e) =>
            {
                if (e.Value) return;
                if (string.IsNullOrEmpty(_inputTextBox.Text) || !int.TryParse(_inputTextBox.Text, out var val))
                {
                    _inputTextBox.Text = this.Value.ToString();
                    return;
                }
                this.Value = val;
            };
            _inputTextBox.TextChanged += (_, _) =>
            {
                if (string.IsNullOrEmpty(_inputTextBox.Text)) return;
                if (!int.TryParse(_inputTextBox.Text, out var val))
                {
                    _inputTextBox.Text = text;
                    _inputTextBox.CursorIndex = text.Length;
                    return;
                }
                text = val.ToString();
            };
        }

        public override void PaintBeforeChildren(SpriteBatch spriteBatch, Rectangle bounds)
        {
            // Draw label
            var textSize = GameService.Content.DefaultFont14.MeasureString(this.Text);
            var labelBounds = new Rectangle(0, 0, (int)textSize.Width, bounds.Height);
            spriteBatch.DrawStringOnCtrl(this, _text, GameService.Content.DefaultFont14, labelBounds, Color.White);

            // Create text box
            var suffixSize = GameService.Content.DefaultFont14.MeasureString(this.Suffix);
            var inputBounds = new Rectangle(labelBounds.Width + 4, 0, bounds.Width - (int)suffixSize.Width - (labelBounds.Width + 4), bounds.Height);
            this.CreateInputTextBox(inputBounds);

            // Draw suffix
            var suffixBounds = new Rectangle(inputBounds.X + inputBounds.Width + 2, 0, (int)suffixSize.Width, bounds.Height);
            spriteBatch.DrawStringOnCtrl(this, this.Suffix, GameService.Content.DefaultFont14, suffixBounds, Color.White);
            base.PaintBeforeChildren(spriteBatch, bounds);
        }
    }
}
