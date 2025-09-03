using System.Text.RegularExpressions;
using YoutubeExplode;

namespace YoutubeVideoDownloader
{
    public partial class YoutubeDownloaderForm : Form
    {
        public YoutubeDownloaderForm()
        {
            InitializeComponent();
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;
            string filePathToSaveVideo = "C:\\Videos";

            string urlFromVideo = txtYoutubeUrl.Text;

            if (string.IsNullOrEmpty(urlFromVideo))
            {
                MessageBox.Show("You must put an url to download!");
                btnDownload.Enabled = true;
                return;
            }

            var youtube = new YoutubeClient();

            var video = await youtube.Videos.GetAsync(urlFromVideo);

            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(urlFromVideo);

            string videoFileName = Path.Combine(filePathToSaveVideo, $"video.mp4");
            string audioFileName = Path.Combine(filePathToSaveVideo, $"audio.mp4");

            string safeTitle = Regex.Replace(video.Title, @"[\s\\/:*?""<>|]", "");
            string outputFileName = Path.Combine(filePathToSaveVideo, $"{safeTitle}.mp4");

            var videoStreamInfo = streamManifest.GetVideoOnlyStreams()
                        .OrderByDescending(s => s.VideoQuality.MaxHeight)
                        .FirstOrDefault();

            var audioStreamInfo = streamManifest.GetAudioOnlyStreams()
                        .Where(s => (s.AudioLanguage is not null && s.AudioLanguage.ToString().Contains("original")) || s.AudioLanguage is null)
                        .OrderByDescending(s => s.Bitrate)
                        .FirstOrDefault();

            if (videoStreamInfo is null || audioStreamInfo is null)
            {
                MessageBox.Show("Error while downloading video, please try again...");
                btnDownload.Enabled = true;
                return;
            }

            try
            {
                await youtube.Videos.Streams.DownloadAsync(videoStreamInfo, videoFileName);

                await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, audioFileName);

                var ffmpeg = new NReco.VideoConverter.FFMpegConverter();
                ffmpeg.Invoke($"-i \"{videoFileName}\" -i \"{audioFileName}\" -c:v copy -c:a aac \"{outputFileName}\"");

                File.Delete(videoFileName);
                File.Delete(audioFileName);

                MessageBox.Show("Download completed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading video: {ex.Message}");
                return;
            }
            finally
            {
                btnDownload.Enabled = true;
            }

            
        }
    }
}
