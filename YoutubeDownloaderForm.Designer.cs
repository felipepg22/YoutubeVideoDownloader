using YoutubeVideoDownloader.Common;

namespace YoutubeVideoDownloader
{
    partial class YoutubeDownloaderForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YoutubeDownloaderForm));
            txtYoutubeUrl = new TextBox();
            btnDownload = new Button();
            lblYoutubeUrl = new Label();
            txtFolderPath = new TextBox();
            lblFolderPath = new Label();
            btnChooseFolder = new Button();
            progressBarDownload = new ProgressBar();
            SuspendLayout();
            // 
            // txtYoutubeUrl
            // 
            txtYoutubeUrl.Location = new Point(13, 127);
            txtYoutubeUrl.Margin = new Padding(4, 5, 4, 5);
            txtYoutubeUrl.Name = "txtYoutubeUrl";
            txtYoutubeUrl.Size = new Size(431, 31);
            txtYoutubeUrl.TabIndex = 0;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(157, 206);
            btnDownload.Margin = new Padding(4, 5, 4, 5);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(159, 42);
            btnDownload.TabIndex = 1;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // lblYoutubeUrl
            // 
            lblYoutubeUrl.AutoSize = true;
            lblYoutubeUrl.Location = new Point(13, 97);
            lblYoutubeUrl.Margin = new Padding(4, 0, 4, 0);
            lblYoutubeUrl.Name = "lblYoutubeUrl";
            lblYoutubeUrl.Size = new Size(162, 25);
            lblYoutubeUrl.TabIndex = 2;
            lblYoutubeUrl.Text = "Youtube video URL";
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(17, 58);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.Size = new Size(378, 31);
            txtFolderPath.TabIndex = 3;
            txtFolderPath.Text = "C:\\Videos";
            // 
            // lblFolderPath
            // 
            lblFolderPath.AutoSize = true;
            lblFolderPath.Location = new Point(13, 30);
            lblFolderPath.Name = "lblFolderPath";
            lblFolderPath.Size = new Size(124, 25);
            lblFolderPath.TabIndex = 4;
            lblFolderPath.Text = "Folder to save";
            // 
            // btnChooseFolder
            // 
            btnChooseFolder.BackgroundImage = (Image)resources.GetObject("btnChooseFolder.BackgroundImage");
            btnChooseFolder.BackgroundImageLayout = ImageLayout.Zoom;
            btnChooseFolder.Location = new Point(401, 58);
            btnChooseFolder.Name = "btnChooseFolder";
            btnChooseFolder.Size = new Size(57, 34);
            btnChooseFolder.TabIndex = 5;
            btnChooseFolder.UseVisualStyleBackColor = true;
            btnChooseFolder.Click += btnChooseFolder_Click;
            // 
            // progressBarDownload
            // 
            progressBarDownload.Location = new Point(12, 164);
            progressBarDownload.Name = "progressBarDownload";
            progressBarDownload.Size = new Size(462, 34);
            progressBarDownload.TabIndex = 6;
            progressBarDownload.Maximum = 100;
            // 
            // YoutubeDownloaderForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 262);
            Controls.Add(progressBarDownload);
            Controls.Add(btnChooseFolder);
            Controls.Add(lblFolderPath);
            Controls.Add(txtFolderPath);
            Controls.Add(lblYoutubeUrl);
            Controls.Add(btnDownload);
            Controls.Add(txtYoutubeUrl);
            Margin = new Padding(4, 5, 4, 5);
            Name = "YoutubeDownloaderForm";
            Text = "Youtube Video Downloader";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtYoutubeUrl;
        private Button btnDownload;
        private Label lblYoutubeUrl;
        private TextBox txtFolderPath;
        private Label lblFolderPath;
        private Button btnChooseFolder;
        private ProgressBar progressBarDownload;
    }
}
