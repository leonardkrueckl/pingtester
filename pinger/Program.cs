using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;

namespace pinger
{
    class Program
    {
        static List<int> pings = new List<int>();
        static string filename = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".txt";

        static void Main(string[] args)
        {

            Thread.Sleep(1000);

            using (StreamWriter sw = File.AppendText(filename))
            {
                while (true)
                {
                    double ping = PingTimeAverage("8.8.8.8", 1);
                    Console.WriteLine(ping);
                    sw.WriteLine(ping);
                    pings.Add((int)ping);
                    sw.Flush();
                    Thread.Sleep(800);
                }
            }               
        }
        
        public static double PingTimeAverage(string host, int echoNum)
        {
            long totalTime = 0;
            Ping pingSender = new Ping();

            for (int i = 0; i < echoNum; i++)
            {
                PingReply reply = pingSender.Send(host);
                if (reply.Status == IPStatus.Success)
                {
                    totalTime += reply.RoundtripTime;
                }
                else Console.WriteLine("Connection timed out");
            }
            return totalTime / echoNum;
        }

        void OnProcessExit(object sender, EventArgs e)
        {
            int pingmedian = 0;
            for (int i = 0; i < pings.Count; i++)
            {
                pingmedian += pings[i];
            }
            pingmedian = pingmedian / pings.Count;

            using (StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine("---------------------");
                sw.WriteLine("Average Ping: " + pingmedian);
            }
        }
    }
}