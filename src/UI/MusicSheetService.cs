using Blish_HUD;
using LiteDB;
using Microsoft.Xna.Framework.Audio;
using Nekres.Musician.Core.Models;
using Nekres.Musician.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nekres.Musician.UI
{
    internal class MusicSheetService : IDisposable
    {
        public event EventHandler<ValueEventArgs<MusicSheetModel>> OnSheetUpdated;

        public string CacheDir { get; private set; }

        private LiteDatabase _db;
        private ILiteCollection<MusicSheetModel> _ctx;

        private SoundEffect[] _deleteSfx;
        public SoundEffect DeleteSfx => _deleteSfx[RandomUtil.GetRandom(0, 1)];

        public MusicSheetService(string cacheDir)
        {
            _deleteSfx = new []
            {
                MusicianModule.ModuleInstance.ContentsManager.GetSound(@"audio\crumbling-paper-1.wav"),
                MusicianModule.ModuleInstance.ContentsManager.GetSound(@"audio\crumbling-paper-2.wav")
            }; 
            this.CacheDir = cacheDir;
        }

        public void LoadDatabase()
        {
            _db = new LiteDatabase(new ConnectionString
            {
                Filename = Path.Combine(this.CacheDir, "data.db"),
                Connection = ConnectionType.Shared
            });
            _ctx = _db.GetCollection<MusicSheetModel>("music_sheets");
        }

        public void AddOrUpdate(MusicSheet musicSheet, bool silent = false)
        {
            var model = musicSheet.ToModel();
            _ctx.Upsert(model);
            _ctx.EnsureIndex(x => x.Id);
            OnSheetUpdated?.Invoke(this, new ValueEventArgs<MusicSheetModel>(model));

            if (silent) return;
            GameService.Content.PlaySoundEffectByName("color-change");
        }

        public void Delete(Guid key)
        {
            DeleteSfx.Play(GameService.GameIntegration.Audio.Volume, 0, 0);
            _ctx.DeleteMany(x => x.Id.Equals(key));
        }

        public void Dispose()
        {
            foreach (var sfx in _deleteSfx) sfx?.Dispose();
            _db?.Dispose();
        }

        public MusicSheetModel GetById(Guid id)
        {
           return _ctx.FindOne(x => x.Id.Equals(id));
        }

        public IEnumerable<MusicSheetModel> GetAll()
        {
            return _ctx.FindAll();
        }
    }
}
