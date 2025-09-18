using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
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
            Video video = await GetVideoInformationAsync(url);

            StreamManifest streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(url);

            string videoFileName = Path.Combine(filePathToSave, $"video.mp4");
            string audioFileName = Path.Combine(filePathToSave, $"audio.mp4");

            string safeTitle = Regex.Replace(video.Title, @"[\s\\/:*?""<>|]", "");
            string outputFileName = Path.Combine(filePathToSave, $"{safeTitle}.mp4");

            VideoOnlyStreamInfo? videoStreamInfo = streamManifest.GetVideoOnlyStreams()
                        .OrderByDescending(s => s.VideoQuality.MaxHeight)
                        .FirstOrDefault();

            AudioOnlyStreamInfo? audioStreamInfo = streamManifest.GetAudioOnlyStreams()
                        .Where(s => (s.AudioLanguage is not null && s.AudioLanguage.ToString().Contains("original")) || s.AudioLanguage is null)
                        .OrderByDescending(s => s.Bitrate)
                        .FirstOrDefault();

            if (videoStreamInfo is null || audioStreamInfo is null)
            {
                throw new DownloadException();
            }

            int videoProgressValue = 50;
            int audioProgressValue = 15;
            int mergingProgressValue = 100 - videoProgressValue - audioProgressValue;

            Progress<double> videoProgress = new Progress<double>(p => progress.Report(p * videoProgressValue));
            Progress<double> audioProgress = new Progress<double>(p => progress.Report(videoProgressValue + (p * audioProgressValue)));

            await _youtubeClient.Videos.Streams.DownloadAsync(videoStreamInfo, 
                                                              videoFileName, 
                                                              videoProgress);

            await _youtubeClient.Videos.Streams.DownloadAsync(audioStreamInfo, 
                                                              audioFileName,
                                                              audioProgress);

            
            var ffmpeg = new NReco.VideoConverter.FFMpegConverter();

            double totalDurationSeconds = video.Duration?.TotalSeconds ?? 0;
            double mergeStart = videoProgressValue + audioProgressValue;
            double mergeEnd = 100;
            ffmpeg.LogReceived += (sender, e) =>
            {
                var match = Regex.Match(e.Data, @"time=(\d{2}):(\d{2}):(\d{2})\.(\d{2})");
                if (match.Success && totalDurationSeconds > 0)
                {
                    int h = int.Parse(match.Groups[1].Value);
                    int m = int.Parse(match.Groups[2].Value);
                    int s = int.Parse(match.Groups[3].Value);
                    int cs = int.Parse(match.Groups[4].Value);
                    double currentSeconds = h * 3600 + m * 60 + s + cs / 100.0;
                    double percent = mergeStart + ((currentSeconds / totalDurationSeconds) * (mergeEnd - mergeStart));
                    if (percent > mergeEnd) percent = mergeEnd;
                    progress.Report(percent);
                }
            };


            await Task.Run(() =>
            {
                ffmpeg.Invoke($"-i \"{videoFileName}\" -i \"{audioFileName}\" -c:v copy -c:a aac \"{outputFileName}\"");
            });
          
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
