using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecureBoxClientNew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == openFileDialog1.ShowDialog())
                {
                    textBox1.Text = openFileDialog1.FileName;
                    string fileName = textBox1.Text;

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:2332/api//");

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    // Create the JSON formatter.
                    MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();

                    // Use the JSON formatter to create the content of the request body.
                    var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(fileName));
                    using (var content = new MultipartFormDataContent())
                    {
                       
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = fileName
                        };
                        content.Add(fileContent);

                        var resp = client.PostAsync("UploadFile", content).Result;
                    }
                    

                    
                }
            }
            catch (Exception ex)
            { 
                
            }
        }

        private void btnTorRequest_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.google.com/");
                request.Proxy = new WebProxy("127.0.0.1",8118); // default privoxy port for tor
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
