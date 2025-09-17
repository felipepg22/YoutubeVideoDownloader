using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeVideoDownloader.Exceptions;

namespace YoutubeVideoDownloader.Services
{
    public class YoutubeDownloadService : IDownloadService
    {
        private readonly YoutubeClient _youtubeClient;

        public YoutubeDownloadService(YoutubeClient youtubeClient)
        {
            _youtubeClient = youtubeClient;
        }

        public async Task DownloadVideoAsync(string url, string filePathToSave, IProgress<double> progress)
        {
            var video = await GetVideoInformationAsync(url);

            var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(url);

            string videoFileName = Path.Combine(filePathToSave, $"video.mp4");
            string audioFileName = Path.Combine(filePathToSave, $"audio.mp4");

            string safeTitle = Regex.Replace(video.Title, @"[\s\\/:*?""<>|]", "");
            string outputFileName = Path.Combine(filePathToSave, $"{safeTitle}.mp4");

            var videoStreamInfo = streamManifest.GetVideoOnlyStreams()
                        .OrderByDescending(s => s.VideoQuality.MaxHeight)
                        .FirstOrDefault();

            var audioStreamInfo = streamManifest.GetAudioOnlyStreams()
                        .Where(s => (s.AudioLanguage is not null && s.AudioLanguage.ToString().Contains("original")) || s.AudioLanguage is null)
                        .OrderByDescending(s => s.Bitrate)
                        .FirstOrDefault();

            if (videoStreamInfo is null || audioStreamInfo is null)
            {
                throw new DownloadException();
            }

            await _youtubeClient.Videos.Streams.DownloadAsync(videoStreamInfo, 
                                                              videoFileName, 
                                                              new Progress<double>(p => progress.Report(p * 0.5)));

            await _youtubeClient.Videos.Streams.DownloadAsync(audioStreamInfo, 
                                                              audioFileName,
                                                              new Progress<double>(p => progress.Report(50 + (p * 0.4))));

            var ffmpeg = new NReco.VideoConverter.FFMpegConverter();
            ffmpeg.Invoke($"-i \"{videoFileName}\" -i \"{audioFileName}\" -c:v copy -c:a aac \"{outputFileName}\"");
            progress.Report(100);

            File.Delete(videoFileName);
            File.Delete(audioFileName);
        }

        public async Task<Video> GetVideoInformationAsync(string url)
        {
            return await _youtubeClient.Videos.GetAsync(url);
        }
    }
}
