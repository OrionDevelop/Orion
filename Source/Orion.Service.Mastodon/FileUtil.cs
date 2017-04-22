using System;
using System.Collections.Generic;
using System.IO;

namespace Orion.Service.Mastodon
{
    internal static class FileUtil
    {
        private static readonly Dictionary<string, string> _mimeDictionary = new Dictionary<string, string>
        {
            // https://github.com/tootsuite/mastodon/blob/master/app/models/media_attachment.rb#L8
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".png", "image/png"},
            {".gif", "image/gif"},
            // https://github.com/tootsuite/mastodon/blob/master/app/models/media_attachment.rb#L9
            {".mp4", "video/mp4"}
        };

        public static string FileToBase64Strings(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(nameof(filePath));
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var byteArray = new byte[stream.Length];
                stream.ReadAsync(byteArray, 0, (int) stream.Length);
                return $"data:{GetMimeType(filePath)};base64,{Convert.ToBase64String(byteArray)}";
            }
        }

        private static string GetMimeType(string filepath)
        {
            var extension = Path.GetExtension(filepath);
            return _mimeDictionary.ContainsKey(extension) ? _mimeDictionary[extension] : "";
        }
    }
}