using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallHandler
{
    public static class Logging
    {



        //teres
        public static void write(string line)
        {
            DateTime now = DateTime.Now;
            if (!Directory.Exists(@"" + Config.LoggingDir + ""))
            {
                Directory.CreateDirectory(@"" + Config.LoggingDir + "");

            }

            using (StreamWriter sw = new StreamWriter(@"" + Config.LoggingDir + Config.LoggFileName + "_" + now.ToString("yyyy-MM-dd") + ".log", true))
            {

                sw.WriteLine(now + " " + line);
                sw.Close();
            }
        }
    }
}
