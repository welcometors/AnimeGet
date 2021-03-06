﻿using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using HAP = HtmlAgilityPack;

namespace AnimeGet
{
    public partial class frmMain : Form
    {
        private static IDictionary<string, string> series = 
            new Dictionary<string, string>() { 
            { "Naruto & Naruto Shippuden Episodes English Dubbed", "http://www.naruget.com/cat/4-english-dubbed/" }, 
            { "Naruto & Naruto Shippuden Episodes", "http://www.naruget.com/" } ,
            { "Dragonball Z English Dubbed", "http://www.dbz.tv/watch-dragonball-z-episodes-dubbed-online/"},
            { "Bleach English Dubbed", "http://www.bleachget.com/page/bleach-episodes-english-dubbed/"},
            { "One Piece English Dubbed", "http://www.watchop.eu/page/one-piece-episodes-english-dubbed/"}
        };

        private ConcurrentQueue<int> _downloadQueue;
        private ConcurrentQueue<int> _detectorQueue;
        private ConcurrentQueue<int> _fetcherQueue;
        private const int MaxParallelDownloads = 4;
        private int currentlyDownloading = 0;

        public frmMain()
        {
            InitializeComponent();
            _downloadQueue = new ConcurrentQueue<int>();
            _detectorQueue = new ConcurrentQueue<int>();
            _fetcherQueue = new ConcurrentQueue<int>();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbSeries.Items.AddRange(series.Keys.ToArray());
            cbSeries.SelectedIndex = 0;
        }

        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void cbSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbUrl.Text = series[cbSeries.Text];
        }

        private void FillEpisodes(IEnumerable<Tuple<string, string>> episodes)
        {
            foreach (var epi in episodes)
                dgvVideos.Rows.Add(false, epi.Item1, null, "-", "[]", epi.Item2, "");
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            dgvVideos.Rows.Clear();
            string url = tbUrl.Text;
            btnFetch.Text = "Fetching...";

            var urlReader = new BackgroundWorker() { WorkerSupportsCancellation = false, WorkerReportsProgress = false };

            urlReader.DoWork += (senderObject, eventArgs) =>
            {
                var episodes = new List<Tuple<string, string>>();
                var web = new HAP.HtmlWeb();
                var doc = web.Load(url);
                var hosturi = (new Uri(url)).Host;
                foreach (var node in doc.DocumentNode.SelectNodes("//a"))
                {
                    if (node.Attributes.Contains("href") && node.Attributes.Contains("class")
                        && node.Attributes["class"].Value == "movie"
                        && node.Attributes["href"].Value.Contains(hosturi))
                    {
                        episodes.Add(new Tuple<string, string>(node.ParentNode.InnerText, node.Attributes["href"].Value));
                    }
                }
                eventArgs.Result = episodes;
            };

            urlReader.RunWorkerCompleted += (senderObject, eventArgs) =>
            {
                var episodes = eventArgs.Result as List<Tuple<string, string>>;
                FillEpisodes(episodes);
                btnFetch.Text = "Fetch!";
            };

            urlReader.RunWorkerAsync();
        }

        private IDictionary<string,string> getVideoUrls(string mainUrl)
        {
            var videoSources = new Dictionary<string, string>();
            var web = new HAP.HtmlWeb();
            var doc = web.Load(mainUrl);
            var videoSelectorTable = doc.DocumentNode.SelectSingleNode("//td[@id='embedcode']").ParentNode.ParentNode;

            foreach (var node in videoSelectorTable.SelectNodes("//a"))
            {
                if (node.Attributes.Contains("onclick"))
                {
                    var jscriptCode = node.Attributes["onclick"].Value;
                    const string pivot = "unescape('";
                    var srtIdx = jscriptCode.IndexOf(pivot) + pivot.Length;
                    var endIdx = jscriptCode.IndexOf("'", srtIdx + 1);
                    var redirectUrlEncoded = jscriptCode.Substring( srtIdx, endIdx - srtIdx);
                    var redirectUrl = WebUtility.UrlDecode(redirectUrlEncoded);
                    var scriptDoc = new HAP.HtmlDocument();
                    scriptDoc.LoadHtml(redirectUrl);
                    string videoPageUrl = scriptDoc.DocumentNode.SelectSingleNode("//iframe").Attributes["src"].Value;
                    videoSources.Add( node.InnerText, videoPageUrl);
                }
            }

            return videoSources;
        }

        private void populateVideoTypes(int index, IDictionary<string,string> types)
        {
            if (0 <= index && index < dgvVideos.Rows.Count)
            {
                dgvVideos.Rows[index].Cells["dgvcType"].Tag = types;
                (dgvVideos.Rows[index].Cells["dgvcType"] as DataGridViewComboBoxCell).DataSource = types.Keys.ToList();
                dgvVideos.Rows[index].Cells["dgvcType"].Value = types.Keys.First();
            }
        }

        private void dgvVideos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVideos.Columns[e.ColumnIndex].Name == "dgvcLinkSelected" && dgvVideos.Rows[e.RowIndex].Cells["dgvcVideo"].Value.ToString() == "")
            {
                dgvVideos.Rows[e.RowIndex].Cells["dgvcStatus"].Value = "Detecting...";
                //dgvVideos.Update();
                //dgvVideos.Refresh();
                _detectorQueue.Enqueue(e.RowIndex);
            }
            else
            if (dgvVideos.Columns[e.ColumnIndex].Name == "dgvcDownload" && dgvVideos.Rows[e.RowIndex].Cells["dgvcDownload"].Value.ToString() == ">")
            {
                dgvVideos.Rows[e.RowIndex].Cells["dgvcDownload"].Value = "||";
                dgvVideos.Rows[e.RowIndex].Cells["dgvcStatus"].Value = "Queued";
                //dgvVideos.Update();
                //dgvVideos.Refresh();
                _downloadQueue.Enqueue(e.RowIndex);
            }
        }

        private void dgvVideos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ( dgvVideos.Columns[e.ColumnIndex].Name == "dgvcType")
            {
                dgvVideos.Rows[e.RowIndex].Cells["dgvcStatus"].Value = "Fetching...";
                //dgvVideos.Update();
                //dgvVideos.Refresh();
                _fetcherQueue.Enqueue(e.RowIndex);
            }
        }

        void myWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            WebClient myWebClient = sender as WebClient;
            int idx = 0;
            if (int.TryParse(myWebClient.QueryString["index"], out idx))
            {
                dgvVideos.Rows[idx].Cells["dgvcStatus"].Value = e.ProgressPercentage.ToString() + "%";
            }
        }

        void myWebClient_DownloadDataCompleted(object sender, AsyncCompletedEventArgs e)
        {
            WebClient myWebClient = sender as WebClient;
            int idx = 0;
            if (int.TryParse(myWebClient.QueryString["index"], out idx))
            {
                dgvVideos.Rows[idx].Cells["dgvcStatus"].Value = "Done!";
                dgvVideos.Rows[idx].Cells["dgvcDownload"].Value = ">";
                currentlyDownloading--;
            }
        }

        private void tmrDetector_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrDetector.Stop();
                if (!_detectorQueue.IsEmpty)
                {
                    int idx = 0;
                    if (_detectorQueue.TryDequeue(out idx))
                    {
                        string episodeUrl = dgvVideos.Rows[idx].Cells["dgvcUrl"].Value.ToString();
                        populateVideoTypes(idx, getVideoUrls(episodeUrl));
                    }
                }
            }
            finally
            {
                tmrDetector.Start();
            }
        }

        private void tmrFetcher_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrFetcher.Stop();
                if (!_fetcherQueue.IsEmpty)
                {
                    int idx = 0;
                    if (_fetcherQueue.TryDequeue(out idx))
                    {
                        var videos = dgvVideos.Rows[idx].Cells["dgvcType"].Tag as Dictionary<string, string>;
                        string pageUrl = videos[dgvVideos.Rows[idx].Cells["dgvcType"].Value as string];
                        var web = new HAP.HtmlWeb();
                        var doc = web.Load(pageUrl);

                        foreach (var node in doc.DocumentNode.SelectSingleNode("//body").ChildNodes)
                        {
                            if (node.Name == "script" && node.InnerHtml.Contains("if(!FlashDetect.installed)"))
                            {
                                string searchString = "<source src=\"";
                                var startIdx = node.InnerHtml.IndexOf(searchString);
                                var endIdx = node.InnerHtml.IndexOf("\"", startIdx + searchString.Length + 1);
                                var videoUrlLength = endIdx - startIdx - searchString.Length;
                                string videoUrl = node.InnerHtml.Substring(startIdx + searchString.Length, videoUrlLength);
                                dgvVideos.Rows[idx].Cells["dgvcVideo"].Value = videoUrl;
                                dgvVideos.Rows[idx].Cells["dgvcStatus"].Value = "Ready!";
                                dgvVideos.Rows[idx].Cells["dgvcDownload"].Value = ">";
                            }
                        }
                    }
                }
            }
            finally
            {
                tmrFetcher.Start();
            }
        }

        private void tmrDownloader_Tick(object sender, EventArgs e)
        {
            try
            {
                tmrDownloader.Stop();
                if (currentlyDownloading < MaxParallelDownloads && !_downloadQueue.IsEmpty)
                {
                    int idx = 0;
                    if (_downloadQueue.TryDequeue(out idx))
                    {
                        dgvVideos.Rows[idx].Cells["dgvcStatus"].Value = "Starting...";

                        Uri url = new Uri(dgvVideos.Rows[idx].Cells["dgvcVideo"].Value.ToString());
                        string fileName = Regex.Replace(dgvVideos.Rows[idx].Cells["dgvcTitle"].Value.ToString().Trim(), "[^a-zA-Z0-9 ]+", "", RegexOptions.Compiled);
                        string filePath = string.Format("{0}\\{1}.mp4", tbPath.Text, fileName);

                        WebClient myWebClient = new WebClient();
                        myWebClient.QueryString = new System.Collections.Specialized.NameValueCollection() { { "index", idx.ToString() } };
                        myWebClient.DownloadFileCompleted +=
                            new AsyncCompletedEventHandler(myWebClient_DownloadDataCompleted);
                        myWebClient.DownloadProgressChanged +=
                            new DownloadProgressChangedEventHandler(myWebClient_DownloadProgressChanged);

                        //if (File.Exists(filePath) == false)
                        //{
                        //    Directory.CreateDirectory(filePath.Substring(0, filePath.LastIndexOf("\\")));
                        myWebClient.DownloadFileAsync(url, filePath);
                        //}

                        currentlyDownloading++;
                    }
                }
            }
            finally
            {
                tmrDownloader.Start();
            }
        }
        
    }
}
