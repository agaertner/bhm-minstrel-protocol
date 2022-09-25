using Blish_HUD;
using Blish_HUD.Controls;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Input;
using Microsoft.Xna.Framework;
using Nekres.Musician.Controls;
using Nekres.Musician.Core.Models;
using Nekres.Musician.UI.Controls;
using Nekres.Musician.UI.Models;
using Nekres.Musician.UI.Presenters;
using System;
using System.Threading.Tasks;

namespace Nekres.Musician.UI.Views
{
    internal class LibraryView : View<LibraryPresenter>
    {
        internal event EventHandler<EventArgs> OnImportFromClipboardClick;

        private const int TOP_MARGIN = 0;
        private const int RIGHT_MARGIN = 5;
        private const int BOTTOM_MARGIN = 10;
        private const int LEFT_MARGIN = 8;

        public FlowPanel MelodyFlowPanel { get; private set; }

        private string _activeFilter;

        public LibraryView(LibraryModel model)
        {
            this.WithPresenter(new LibraryPresenter(this, model));
            _activeFilter = MusicianModule.ModuleInstance.SheetFilter.Value;
        }

        protected override async Task<bool> Load(IProgress<string> progress)
        {
            return await Task.Run(() =>
            {
                progress.Report(MusicianModule.ModuleInstance.MusicSheetImporter.Log);
                return !MusicianModule.ModuleInstance.MusicSheetImporter.IsLoading;
            });
        }

        protected override async void Build(Container buildPanel)
        {
            var searchBar = new TextBox
            {
                Parent = buildPanel,
                MaxLength = 256,
                Location = new Point(0, TOP_MARGIN),
                Size = new Point(150, 32),
                PlaceholderText = "Search..."
            };
            searchBar.Hide();
            searchBar.EnterPressed += OnSearchFilterChanged;

            // Sort drop down
            var ddSortMethod = new Dropdown
            {
                Parent = buildPanel,
                Location = new Point(buildPanel.ContentRegion.Width - RIGHT_MARGIN - 150, TOP_MARGIN),
                Width = 150
            };
            ddSortMethod.Hide();
            ddSortMethod.Items.Add(this.Presenter.Model.DD_TITLE);
            ddSortMethod.Items.Add(this.Presenter.Model.DD_ARTIST);
            ddSortMethod.Items.Add(this.Presenter.Model.DD_USER);
            ddSortMethod.Items.Add("------------------");
            foreach (var instrument in Enum.GetNames(typeof(Instrument)))
                ddSortMethod.Items.Add(instrument);
            ddSortMethod.ValueChanged += OnSortChanged;

            MelodyFlowPanel = new FlowPanel
            {
                Parent = buildPanel,
                Location = new Point(0, ddSortMethod.Bottom + BOTTOM_MARGIN),
                Size = new Point(buildPanel.ContentRegion.Width - 10, buildPanel.ContentRegion.Height - 150),
                FlowDirection = ControlFlowDirection.LeftToRight,
                ControlPadding = new Vector2(5, 5),
                CanCollapse = false,
                CanScroll = true,
                Collapsed = false,
                ShowTint = true,
                ShowBorder = true
            };
            MelodyFlowPanel.Hide();

            var loading = new LoadingSpinner
            {
                Parent = buildPanel,
                Size = new Point(64, 64),
                Location = new Point((buildPanel.ContentRegion.Width - 64) / 2, (buildPanel.ContentRegion.Height - 64) / 2)
            };

            var initialSheets = await MusicianModule.ModuleInstance.MusicSheetService.GetAll();
            foreach (var sheet in initialSheets)
            {
                loading.BasicTooltipText = $"Loading {sheet.Title}";
                this.CreateSheetButton(sheet);
            }
            loading.Dispose();
            MelodyFlowPanel.Show();
            searchBar.Show();
            ddSortMethod.Show();

            var importBtn = new ClipboardButton
            {
                Parent = buildPanel,
                Size = new Point(42, 42),
                Location = new Point((buildPanel.ContentRegion.Width - 42 ) / 2, MelodyFlowPanel.Bottom + BOTTOM_MARGIN),
                BasicTooltipText = "Import XML from Clipboard"
            };
            importBtn.Click += OnImportFromClipboardBtnClick;
            ddSortMethod.SelectedItem = _activeFilter;
        }

        public SheetButton CreateSheetButton(MusicSheetModel model)
        {
            var sheetBtn = new SheetButton(model)
            {
                Parent = MelodyFlowPanel
            };
            sheetBtn.OnPreviewClick += OnPreviewClick;
            sheetBtn.OnEmulateClick += OnEmulateClick;
            sheetBtn.OnDelete += OnDeleteClick;
            return sheetBtn;
        }

        private async void OnPreviewClick(object o, ValueEventArgs<bool> e)
        {
            
            var sheetBtn = (SheetButton)o;
            if (e.Value) {
                var sheet = await MusicianModule.ModuleInstance.MusicSheetService.GetById(sheetBtn.Id);
                await MusicianModule.ModuleInstance.MusicPlayer.PlayPreview(MusicSheet.FromModel(sheet));
            }
            else
                MusicianModule.ModuleInstance.MusicPlayer.Stop();
        }

        private async void OnEmulateClick(object o, EventArgs e)
        {
            var sheetBtn = (SheetButton)o;
            var sheet = await MusicianModule.ModuleInstance.MusicSheetService.GetById(sheetBtn.Id);
            MusicianModule.ModuleInstance.MusicPlayer.PlayEmulate(MusicSheet.FromModel(sheet));
        }

        private async void OnDeleteClick(object o, ValueEventArgs<Guid> e)
        {
            await MusicianModule.ModuleInstance.MusicSheetService.Delete(e.Value);
        }

        private void OnImportFromClipboardBtnClick(object o, MouseEventArgs e)
        {
            OnImportFromClipboardClick?.Invoke(this, EventArgs.Empty);
        }

        private void OnSearchFilterChanged(object o, EventArgs e)
        {
            var text = ((TextBox)o).Text;
            text = string.IsNullOrEmpty(text) ? text : text.ToLowerInvariant();
            this.MelodyFlowPanel.SortChildren<SheetButton>((x, y) =>
            {
                x.Visible = string.IsNullOrEmpty(text) || (x.Title + " - " + x.Artist).ToLowerInvariant().Contains(text) || x.User.ToLowerInvariant().Contains(text);
                y.Visible = string.IsNullOrEmpty(text) || (y.Title + " - " + y.Artist).ToLowerInvariant().Contains(text) || y.User.ToLowerInvariant().Contains(text);

                if (!x.Visible || !y.Visible) return 0;

                if (MusicianModule.ModuleInstance.SheetFilter.Value.Equals(this.Presenter.Model.DD_ARTIST))
                    return string.Compare(x.Artist, y.Artist, StringComparison.InvariantCultureIgnoreCase);
                if (MusicianModule.ModuleInstance.SheetFilter.Value.Equals(this.Presenter.Model.DD_TITLE))
                    return string.Compare(x.Title, y.Title, StringComparison.InvariantCultureIgnoreCase);
                return string.Compare(x.User, y.User, StringComparison.InvariantCultureIgnoreCase);
            });
        }

        private void OnSortChanged(object o, ValueChangedEventArgs e)
        {
            MusicianModule.ModuleInstance.SheetFilter.Value = e.CurrentValue;
            SortSheetButtons(e.CurrentValue);
        }

        private void SortSheetButtons(string filter)
        {
            MelodyFlowPanel?.SortChildren<SheetButton>((x, y) =>
            {
                var isInstrument = Enum.TryParse<Instrument>(filter, true, out _);
                x.Visible = !isInstrument || x.Instrument.ToString().Equals(filter, StringComparison.InvariantCultureIgnoreCase);
                y.Visible = !isInstrument || y.Instrument.ToString().Equals(filter, StringComparison.InvariantCultureIgnoreCase);

                if (!x.Visible || !y.Visible) return 0;

                if (this.Presenter.Model.DD_TITLE.Equals(filter))
                    return string.Compare(x.Title, y.Title, StringComparison.InvariantCulture);
                if (this.Presenter.Model.DD_ARTIST.Equals(filter))
                    return string.Compare(x.Artist, y.Artist, StringComparison.InvariantCulture);
                if (this.Presenter.Model.DD_USER.Equals(filter))
                    return string.Compare(x.User, y.User, StringComparison.InvariantCulture);
                return 0;
            });
        }
    }
}
