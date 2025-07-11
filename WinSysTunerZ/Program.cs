using System;
using System.Windows;
using Velopack;

namespace WinSysTunerZ
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            VelopackApp.Build().Run(); // <- Muss VOR deiner App-Instanz stehen!
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
