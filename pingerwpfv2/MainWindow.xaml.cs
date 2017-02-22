using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace pingerwpfv2
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<int> pings = new List<int>();
        int pinger = 0;
        int pingadder = 0;
        static string filename = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".txt";
        StreamWriter sw = File.AppendText(filename);
        int pingmedian = 0;
        double ping = 0;


        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(800);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try {
                double ping = PingTimeAverage("8.8.8.8");
                if (ping != 1337420) lPing.Content = ping;
                else lPing.Content = "Connection timed out";
                sw.WriteLine(ping);
                sw.Flush();

                pingadder += (int)ping;
                pinger++;
                pingmedian = pingadder / pinger;
                lPingMedian.Content = pingmedian;

                if (ping < 70) lPing.Foreground = Brushes.White;
                else if (ping > 70 && ping < 100) lPing.Foreground = Brushes.Yellow;
                else if (ping > 100) lPing.Foreground = Brushes.Red;
                if (pingmedian < 70) lPingMedian.Foreground = Brushes.White;
                else if (pingmedian > 70 && pingmedian < 100) lPingMedian.Foreground = Brushes.Yellow;
                else if (pingmedian > 100) lPingMedian.Foreground = Brushes.Red;

            }
            catch (Exception end)
            {
                MessageBox.Show(end.Message);
            }            
        }

        public static double PingTimeAverage(string host)
        {
            long totalTime = 0;
            Ping pingSender = new Ping();

           
            PingReply reply = pingSender.Send(host);
            if (reply.Status == IPStatus.Success)
            {
                totalTime += reply.RoundtripTime;
            }
            else {
                totalTime = 1337420;
            }
            return totalTime;
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            sw.WriteLine("------------------------------------------------------");
            sw.WriteLine(pingmedian);
            sw.Flush();

            Thread.Sleep(500);
            base.OnClosing(e);
        }

    }
}
