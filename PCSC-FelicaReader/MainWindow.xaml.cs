using PCSC_FelicaReader.ViewModels;
using System;
using System.Windows;

namespace PCSC_FelicaReader
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        SmartCard.DeviceManager deviceManager = new SmartCard.DeviceManager();

        public MainWindow()
        {
            InitializeComponent();

            var device = new DeviceModel();
            DataContext = device;

            deviceManager.OnStatusChange = (isRunning) =>
            {
                device.IsRunning.Value = isRunning;
            };
            deviceManager.OnError = (error) =>
            {
                device.Message.Value = error.ToString();
            };
            deviceManager.OnCardPresented = (reader, card) =>
            {
                try
                {
                    device.Reader.SerialNumber.Value = reader.ReadSerialNumber();
                    device.Card.IDm.Value = card.GetIDm();
                }
                catch (Exception e)
                {
                    device.Message.Value = e.ToString();
                }
            };
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            deviceManager.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            var device = DataContext as DeviceModel;
            device.Reader.SerialNumber.Value = "";
            device.Card.IDm.Value = "";

            deviceManager.Stop();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            var device = DataContext as DeviceModel;
            device.Reader.SerialNumber.Value = "";
            device.Card.IDm.Value = "";

            deviceManager.Restart();
        }
    }
}
