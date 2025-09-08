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
            txtYoutubeUrl = new TextBox();
            btnDownload = new Button();
            lblYoutubeUrl = new Label();
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
            btnDownload.Location = new Point(151, 168);
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
            // YoutubeDownloaderForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 242);
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
    }
}
