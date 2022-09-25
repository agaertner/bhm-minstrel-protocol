using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Graphics.UI;
using Nekres.Musician.Controls;
using Nekres.Musician.Core.Models;
using Nekres.Musician.UI.Models;
using Nekres.Musician.UI.Views;
using System;
using System.Linq;

namespace Nekres.Musician.UI.Presenters
{
    internal class LibraryPresenter : Presenter<LibraryView, LibraryModel>
    {
        public LibraryPresenter(LibraryView view, LibraryModel model) : base(view, model)
        {
            view.OnImportFromClipboardClick += View_ImportFromClipboardClicked;
            model.MusicSheetService.OnSheetUpdated += OnSheetUpdated;
        }

        protected override void Unload()
        {
            this.View.OnImportFromClipboardClick -= View_ImportFromClipboardClicked;
            this.Model.MusicSheetService.OnSheetUpdated -= OnSheetUpdated;
            base.Unload();
        }

        private void OnSheetUpdated(object o, ValueEventArgs<MusicSheetModel> e)
        {
            if (!TryGetSheetButtonById(e.Value.Id, out var button))
            {
                this.View.CreateSheetButton(e.Value);
                return;
            }
            button.Artist = e.Value.Artist;
            button.Title = e.Value.Title;
            button.User = e.Value.User;
        }
        
        private bool TryGetSheetButtonById(Guid id, out SheetButton button)
        {
            button = this.View.MelodyFlowPanel?.Children.Where(x => x.GetType() == typeof(SheetButton)).Cast<SheetButton>().FirstOrDefault(y => y.Id.Equals(id));
            if (button == null) return false;
            return true;
        }

        private async void View_ImportFromClipboardClicked(object o, EventArgs e)
        {
            var xml = await ClipboardUtil.WindowsClipboardService.GetTextAsync();
            if (!MusicSheet.TryParseXml(xml, out var sheet))
            {
               GameService.Content.PlaySoundEffectByName("error");
               ScreenNotification.ShowNotification("Your clipboard does not contain a valid music sheet.", ScreenNotification.NotificationType.Error);
               return;
            }
            await MusicianModule.ModuleInstance.MusicSheetService.AddOrUpdate(sheet);
        }
    }
}
