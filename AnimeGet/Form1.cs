using System;
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
        public static IDictionary<string, string> series = 
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
                    dgvVideos.Rows.Add(false, node.ParentNode.InnerText, "Default", "-", node.Attributes["href"].Value, "");
                }
                text += node.InnerHtml;
            }
            
        }

        private void dgvVideos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && dgvVideos.Rows[e.RowIndex].Cells["dgvcVideo"].Value.ToString() == "")
            {
                string url = dgvVideos.Rows[e.RowIndex].Cells["dgvcUrl"].Value.ToString();
                var web = new HAP.HtmlWeb();
                var doc = web.Load(url);
                dgvVideos.Rows[e.RowIndex].Cells["dgvcVideo"].Value = doc.DocumentNode.OuterHtml.Contains("FlashDetect.installed");
                //MessageBox.Show(doc.DocumentNode.SelectSingleNode("//iframe"));
            }
        }
    }
}
