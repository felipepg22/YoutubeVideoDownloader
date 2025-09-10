using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeVideoDownloader.Exceptions
{
    public class DownloadException : Exception
    {
        public override string Message => "Error while downloading";
    }
}
