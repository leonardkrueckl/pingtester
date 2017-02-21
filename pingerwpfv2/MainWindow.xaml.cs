using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pingerwpfv2
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<int> pings = new List<int>();
        static string filename = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".txt";


        public MainWindow()
        {
            InitializeComponent();
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
                else {
                    Console.WriteLine("Connection timed out");
                    totalTime = 1337;
                }
            }
            return totalTime / echoNum;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            lPing.Content = ""+PingTimeAverage("8.8.8.8", 1);
        }


        private void doPings()
        {
            StreamWriter sw = File.AppendText(filename);

            while (true)
            {
                double ping = PingTimeAverage("8.8.8.8", 1);
                lPing.Content = "" + ping;
                sw.WriteLine(ping);
                pings.Add((int)ping);
                sw.Flush();
                Thread.Sleep(800);
            }
        }

        private void lPing_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            doPings();
        }
    }
}
