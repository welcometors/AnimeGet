using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public frmMain()
        {
            InitializeComponent();
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

        private void btnFetch_Click(object sender, EventArgs e)
        {
            dgvVideos.Rows.Clear();
            string url = tbUrl.Text;
            var web = new HAP.HtmlWeb();
            var doc = web.Load(url);
            var hosturi = (new Uri(url)).Host;
            
            string text = "";
            foreach (var node in doc.DocumentNode.SelectNodes("//a"))
            {
                if (node.Attributes.Contains("href") && node.Attributes.Contains("class")
                    && node.Attributes["class"].Value == "movie"
                    && node.Attributes["href"].Value.Contains(hosturi))
                {
                    dgvVideos.Rows.Add(false, node.ParentNode.InnerText, null, "-", node.Attributes["href"].Value, "");
                }
                text += node.InnerHtml;
            }
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
            if (e.ColumnIndex == 0 && dgvVideos.Rows[e.RowIndex].Cells["dgvcVideo"].Value.ToString() == "")
            {
                dgvVideos.Rows[e.RowIndex].Cells["dgvcStatus"].Value = "Detecting...";
                dgvVideos.Update();
                dgvVideos.Refresh();

                string episodeUrl = dgvVideos.Rows[e.RowIndex].Cells["dgvcUrl"].Value.ToString();
                populateVideoTypes(e.RowIndex, getVideoUrls(episodeUrl));
            }
        }

        private void dgvVideos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ( dgvVideos.Columns[e.ColumnIndex].Name == "dgvcType")
            {
                dgvVideos.Rows[e.RowIndex].Cells["dgvcStatus"].Value = "Fetching...";
                dgvVideos.Update();
                dgvVideos.Refresh();
                var videos = dgvVideos.Rows[e.RowIndex].Cells["dgvcType"].Tag as Dictionary<string,string>;
                string pageUrl = videos[dgvVideos.Rows[e.RowIndex].Cells["dgvcType"].Value as string];
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
                        dgvVideos.Rows[e.RowIndex].Cells["dgvcVideo"].Value = videoUrl;
                        dgvVideos.Rows[e.RowIndex].Cells["dgvcStatus"].Value = "Ready!";
                    }
                }
            }
        }
    }
}
