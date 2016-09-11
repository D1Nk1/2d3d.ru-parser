using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using HtmlAgilityPack;

namespace _2d3d.ru_parser
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        struct ForSave
        {
            string name;
            string link;
        }

        private KeyValuePair<string, bool> GET(string url)
        {
            Uri target = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(target);

            request.CookieContainer = new CookieContainer();
            
            request.Method = WebRequestMethods.Http.Get;
            request.CookieContainer.Add(new Cookie("_ym_uid", "1458213639782576458") { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("_ym_isad", "1") { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("LB_member_sc", "6bc09d2fe5de24d3943c25a6b192ef0a") { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("dle_user_id", "150359") { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("dle_password", "46c4e0cb1f6d18376a97b6c1d3d751e5") { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("PHPSESSID", "bcfdb1c6ef0a4682e632c1bf276650da") { Domain = target.Host });
            request.CookieContainer.Add(new Cookie("dle_newpm", "0") { Domain = target.Host });


            HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(httpResponse.StatusCode.ToString());
            if(httpResponse.StatusCode != HttpStatusCode.OK)
            {
                return new KeyValuePair<string, bool>("", false);
            }

            Stream httpResponseStream = httpResponse.GetResponseStream();
            byte[] temp = new byte[1024];

            int bytesRead = 0;
            string res = "";
            while ((bytesRead = httpResponseStream.Read(temp, 0, 1024)) != 0)
            {
                res += Encoding.Default.GetString(temp);
            }
            
            return new KeyValuePair<string, bool>(res, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //"http://www.2d-3d.ru/engine/download.php?id=3203"

            richTextBox1.Text = "";

            for (int i = 1; i < 126; i++)
            {

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    KeyValuePair<string, bool> ans = GET("http://www.2d-3d.ru/2d-galereia/page/" + i.ToString() + "/");
                    if (ans.Value)
                    {
                        doc.LoadHtml(ans.Key);
                        HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='shortstory']/p[@class='lead']/a");
                    if (nodes != null)
                    {
                        foreach (var node in nodes)
                        {
                            richTextBox1.Text += node.Attributes["href"].Value + "\n";
                        }
                    }
                        

                    } else
                    {
                        //FUCKUPHERE
                    }
                    

                richTextBox1.Text += "\n\n\n";
            }

            MessageBox.Show("DONE");

            //int bufferSize = 1024;
            //byte[] buffer = new byte[bufferSize];
            //int bytesRead = 0;
            
            //FileStream fileStream = File.Create("try.rar");
            //while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
            //{
            //    fileStream.Write(buffer, 0, bytesRead);
            //}

        }
    }
}


