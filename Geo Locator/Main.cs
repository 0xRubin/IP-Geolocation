using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text;

namespace Geo_Locator
{
    public partial class Main : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
        );
        Point lastPoint;
        public Main()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 18, 18));
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var url = "https://discord.gg/xP6fw8h93P";
            var process = new System.Diagnostics.ProcessStartInfo();
            process.UseShellExecute = true;
            process.FileName = url;
            System.Diagnostics.Process.Start(process);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string locip = string.Empty;
                string ipaddress = textBox1.Text;

                var client = new RestClient("https://ipapi.co/" + ipaddress + "/json/");
                var request = new RestRequest()
                {
                    Method = Method.GET
                };

                var response = client.Execute(request);

                var dic = JsonConvert.DeserializeObject<IDictionary>(response.Content);

                foreach (var key in dic.Keys)
                {
                    locip += key.ToString() + ":" + dic[key] + "\r\n";
                }

                var ip = dic["ip"];
                var city = dic["city"];
                var region = dic["region"];
                var country = dic["country_name"];
                var postal = dic["postal"];
                var latitude = dic["latitude"];
                var longitude = dic["longitude"];
                var currency = dic["currency_name"];
                var org = dic["org"];

                label3.Text = Convert.ToString("IP: " + ip);
                label4.Text = Convert.ToString("Country: " + country);
                label5.Text = Convert.ToString("City: " + city);
                label6.Text = Convert.ToString("Postal Code: " + postal);
                label7.Text = Convert.ToString("Region: " + region);
                label8.Text = Convert.ToString("Currency: " + currency);
                label9.Text = Convert.ToString("Lat/Long: " + latitude + " - " + longitude);
                label10.Text = Convert.ToString("Organisation: " + org);

            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("ArgumentNullException", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("NullReferenceException", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Exception", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
