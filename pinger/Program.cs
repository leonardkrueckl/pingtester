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
        static void Main(string[] args)
        {
            string filename = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute+ DateTime.Now.Second + ".txt";

            Thread.Sleep(1000);

            using (StreamWriter sw = File.AppendText(filename))
            {
                while (true)
                {
                    double ping = PingTimeAverage("8.8.8.8", 1);
                    Console.WriteLine(ping);
                    sw.WriteLine(ping);
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
        
    }
}