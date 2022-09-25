using System;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Security;
using System.Threading.Tasks;
using Blish_HUD;

namespace Nekres.Musician
{
    internal static class FileUtil
    {
        private static readonly Logger Logger = Logger.GetLogger(typeof(FileUtil));
        public const int FileTimeOutMilliseconds = 10000;
        public static async Task WriteAllTextAsync(string filePath, string data, bool overwrite = true)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            if (!overwrite && File.Exists(filePath))
                return;

            data ??= string.Empty;

            try
            {
                using var sw = new StreamWriter(filePath);
                await sw.WriteAsync(data);
            } catch (ArgumentException aEx) {
                Logger.Error(aEx.Message);
            } catch (UnauthorizedAccessException uaEx) {
                Logger.Error(uaEx.Message);
            } catch (IOException ioEx) {
                Logger.Error(ioEx.Message);
            }
        }

        public static async Task<bool> DeleteAsync(string filePath, bool sendToRecycleBin = true)
        {
            return await Task.Run(() => {
                var timeout = DateTime.UtcNow.AddMilliseconds(FileTimeOutMilliseconds);
                while (DateTime.UtcNow < timeout)
                {
                    try
                    {
                        if (sendToRecycleBin)
                            FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
                        else
                            File.Delete(filePath);
                        return true;
                    }
                    catch (Exception e) when (e is IOException or UnauthorizedAccessException or SecurityException)
                    {
                        if (DateTime.UtcNow < timeout) continue;
                        Logger.Error(e, e.Message);
                        break;
                    }
                }
                return false;
            });
        }
    }
}
