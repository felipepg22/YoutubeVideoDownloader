using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode.Videos;

namespace YoutubeVideoDownloader.Services
{
    public interface IDownloadService
    {
        public Task<Video> GetVideoInformationAsync(string url);

        public Task DownloadVideoAsync(string url, string filePathToSave, IProgress<double> progress);
    }
}
