using Nekres.Musician.Core.Models;
using Nekres.Musician.UI;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nekres.Musician
{
    internal class MusicSheetImporter : IDisposable
    {
        private readonly FileSystemWatcher _xmlWatcher;

        private readonly MusicSheetService _sheetService;

        private readonly IProgress<string> _loadingIndicator;

        public bool IsLoading { get; private set; }

        public string Log { get; private set; }

        public MusicSheetImporter(MusicSheetService sheetService, IProgress<string> loadingIndicator)
        {
            _sheetService = sheetService;
            _loadingIndicator = loadingIndicator;
            _xmlWatcher = new FileSystemWatcher(sheetService.CacheDir)
            {
                NotifyFilter = NotifyFilters.LastWrite,
                Filter = "*.xml",
                EnableRaisingEvents = true
            };
            _xmlWatcher.Created += OnXmlCreated;
        }

        private async void OnXmlCreated(object sender, FileSystemEventArgs e) => await ImportFromFile(e.FullPath);

        public void Init()
        {
            var thread = new Thread(LoadSheetsInBackground)
            {
                IsBackground = true
            };

            thread.Start();
        }

        private async void LoadSheetsInBackground()
        {
            this.IsLoading = true;
            var initialFiles = Directory.EnumerateFiles(_sheetService.CacheDir).Where(s => Path.GetExtension(s).Equals(".xml"));
            foreach (var filePath in initialFiles)
            {
                if (!MusicianModule.ModuleInstance.Loaded) break;
                await ImportFromFile(filePath, true);
            }
            this.IsLoading = false;
            this.Log = null;
            _loadingIndicator.Report(null);
        }

        private async Task ImportFromFile(string filePath, bool silent = false)
        {
            var log = $"Importing {Path.GetFileName(filePath)}..";
            System.Diagnostics.Debug.WriteLine(log);
            MusicianModule.Logger.Info(log);
            this.Log = log;
            _loadingIndicator.Report(log);
            var sheet = MusicSheet.FromXml(filePath);
            if (sheet == null) return;
            await FileUtil.DeleteAsync(filePath);
            await AddToDatabase(sheet, silent);
        }

        internal async Task ImportFromStream(Stream stream, bool silent = false)
        {
            var buffer = new byte[stream.Length];
            var read = await stream.ReadAsync(buffer, 0, buffer.Length);
            var content = System.Text.Encoding.UTF8.GetString(buffer);
            if (!MusicSheet.TryParseXml(content, out var sheet)) return;
            await AddToDatabase(sheet, silent);
            stream.Dispose();
        }

        private async Task AddToDatabase(MusicSheet sheet, bool silent)
        {
            try
            {
                await _sheetService.AddOrUpdate(sheet, silent);
            }
            catch (ObjectDisposedException)
            {
                // Module was unloaded
            }
        }

        public void Dispose()
        {
            _xmlWatcher.Created -= OnXmlCreated;
            _xmlWatcher?.Dispose();
        }
    }
}
