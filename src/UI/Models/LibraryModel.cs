using System;

namespace Nekres.Musician.UI.Models
{
    internal class LibraryModel : IDisposable
    {
        public readonly string DD_TITLE = "Title";
        public readonly string DD_ARTIST = "Artist";
        public readonly string DD_USER = "User";

        public readonly MusicSheetService MusicSheetService;
        public LibraryModel(MusicSheetService sheetService)
        {
            MusicSheetService = sheetService;
        }

        public void Dispose()
        {
        }
    }
}
