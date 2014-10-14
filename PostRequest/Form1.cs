using MongoDB.Bson;
using NAudio.Lame;
using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace PostRequest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://localhost/api/calls");
            request.ContentType = "application/json";
            request.Method = "POST";
            //  var bsonDocument = new BsonDocument();
            //  bsonDocument["startTime"] = DateTime.Now;
            //  bsonDocument.Add("inOut", "in");
            //  bsonDocument.Add("duration", "44");
            //  bsonDocument.Add("aNumber", "5108008");
            // bsonDocument.Add("bNumber", "5284153");
            // bsonDocument.Add("username", "Rõkovanov");
            //  bsonDocument.Add("service", "TalQms");
            //   var json1 = bsonDocument.ToJson();
            //  listBox1.Items.Add("bson  " +json1);


            DateTime now = DateTime.Now;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {

                    startTime = now.ToString("MM/dd/yyyy hh:mm:ss"),
                    inOut = "in",
                    duration = "2555",
                    aNumber = "5108008",
                    bNumber = "5284153",
                    username = "Vladimir Rõkovanov",
                    service = "TalQms"



                });

                listBox1.Items.Add("json " + json);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                listBox1.Items.Add("result " + result);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var reader = new MediaFoundationReader("C:\\temp\\salv\\20140930132422_001_26341929.wav"))
            {
                MediaFoundationEncoder.EncodeToAac(reader, "C:\\temp\\salv\\encodedfile.mp4");
            }
        }

        private void ReadJson_Click(object sender, EventArgs e)
        {
            string dir = "C:\\temp\\salv\\";
            var request = (HttpWebRequest)WebRequest.Create("http://localhost/api/calls");
            request.ContentType = "application/json";
            request.Method = "POST";
            var file=Directory.GetFiles(dir ,"*.json",SearchOption.TopDirectoryOnly);
                foreach (var fileName in file)
	{

        listBox1.Items.Add(fileName);
	}
            
            using (var reader = new WaveFileReader(dir + "20140930132422_001_26341929.wav"))
            using (var writer = new LameMP3FileWriter(dir + "20140930132422_001_26341929.mp3", reader.WaveFormat, 16))
                reader.CopyTo(writer);
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                StreamReader r = new StreamReader(dir + "20140930132422_001_26341929.json");
                string json = r.ReadToEnd();
                JObject o = JObject.Parse(json);
                listBox1.Items.Add("see on test" + o["id"]);
                streamWriter.Write(json);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                listBox1.Items.Add("result " + result);
            }

        }
    }
}
