using System.Text.RegularExpressions;
using YoutubeExplode;
using YoutubeVideoDownloader.Services;

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
            progressBarDownload.Value = 0;
            string filePathToSaveVideo = txtFolderPath.Text.Trim();

            string urlFromVideo = txtYoutubeUrl.Text;

            if (string.IsNullOrEmpty(urlFromVideo))
            {
                MessageBox.Show("You must put an url to download!");
                btnDownload.Enabled = true;
                return;
            }

            
            try
            {
                var progress = new Progress<double>(value =>
                {
                    progressBarDownload.Value = (int)Math.Round(value);
                });

                var youtubeDownloadService = new YoutubeDownloadService(new YoutubeClient());

                await youtubeDownloadService.DownloadVideoAsync(urlFromVideo, filePathToSaveVideo, progress);
                MessageBox.Show("Download completed successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                btnDownload.Enabled = true;
                progressBarDownload.Value = 0;
            }


        }

        private void btnChooseFolder_Click(object sender, EventArgs e)
        {
            using var dialog = new FolderBrowserDialog();
            dialog.Description = "Select a folder to save the video";
            dialog.UseDescriptionForTitle = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = dialog.SelectedPath;
            }
        }
    }
}
