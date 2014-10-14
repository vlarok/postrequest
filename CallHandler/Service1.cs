using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CallHandler
{
    public partial class Service1 : ServiceBase
    {
        Timer myTimer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            myTimer = new Timer(1000 * Convert.ToInt32(Config.SleepTime));
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
            myTimer.Enabled = true;
            myTimer.AutoReset = true;
            myTimer.Start();
            Logging.write("Call Handler started");
        }

        private void myTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
        
            
            var request = (HttpWebRequest)WebRequest.Create("http://localhost/api/calls");
            request.ContentType = "application/json";
            request.Method = "POST";
            var file = Directory.GetFiles(Config.Incoming, "*.json", SearchOption.TopDirectoryOnly);
            foreach (var fileName in file)
            {

                if (fileName.Length != 0)
                    Logging.write(fileName);
            }
        }

        protected override void OnStop()
        {
        }
    }
}
