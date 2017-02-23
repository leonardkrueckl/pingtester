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
        long pingadder = 0;
        static string filename = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".txt";

        DateTime begin = DateTime.Now;

        StreamWriter sw = File.AppendText(filename);
        int pingmedian = 0;
        int pingjumps=0;
        int lastping=1337420;


        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(15);
            timer2.Tick += timer2_Tick;
            timer2.Start();


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(800);
            timer.Tick += timer_Tick;
            timer.Start();

            TimeSpan duration = DateTime.Now.Subtract(begin);
            lDuration.Content = duration.Hours + ":" + duration.Minutes + ":" + duration.Seconds;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try {
                double ping = PingTimeAverage("8.8.8.8");
                pings.Add((int)ping);
                if (ping != 1337420) lPing.Content = ping;
                else lPing.Content = "Connection timeout";
                sw.WriteLine(ping);
                sw.Flush();

                if (ping>lastping+30) 
                {
                    pingjumps++;
                }
                lPingJumps.Content = pingjumps;
                
                pingadder += (long)ping;
                pinger++;
                pingmedian = (int)(pingadder / pinger);
                lPingMedian.Content = pingmedian;

                ChangeColorOfLabel(lPing, ping);
                ChangeColorOfLabel(lPingMedian, pingmedian);
                lastping = (int)ping;
            }
            catch (Exception end)
            {
                MessageBox.Show(end.Message);
                Thread.Sleep(1000);
                base.Close();
            }            
        }

        private void ChangeColorOfLabel(System.Windows.Controls.Label lab, double ping)
        {
            if (ping < 50) lab.Foreground = Brushes.Green;
            else if (ping < 70) lab.Foreground = Brushes.White;
            else if (ping > 70 && ping < 100) lab.Foreground = Brushes.Yellow;
            else if (ping > 100) lab.Foreground = Brushes.Red;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(pings.IndexOf(1337420) != -1) pings.Remove(1337420);

            List<int> pinglist = pings;
            pinglist.Sort();
            lPingBest.Content = pinglist[0];
            lPingWorst.Content = pinglist[pinglist.Count - 1];

            TimeSpan duration = DateTime.Now.Subtract(begin);
            lDuration.Content = duration.Hours + ":" + duration.Minutes + ":" + duration.Seconds;
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
            else totalTime = 1337420;

            return totalTime;
        }
        
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            TimeSpan duration = DateTime.Now.Subtract(begin);

            sw.WriteLine("-----------------");
            sw.WriteLine("Average: " +pingmedian);
            sw.Flush();

            
            base.OnClosing(e);
        }
    }
}
