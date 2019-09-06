﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using RestSharp;
using Newtonsoft.Json;

namespace WesleyAlipio.WeatherPanel.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public class WeatherArea
        {
            public string Latitude { get; set; }

            public string Longitude { get; set; }

            public CurrentWeather Currently { get; set; }
        }
        public class CurrentWeather
        {
            public string Summary { get; set; }

            public string Humidity { get; set; }

            public string WindSpeed { get; set; }

            public string Temperature { get; set; }

            public string Pressure { get; set; }

            public string Icon { get; set; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://api.darksky.net/forecast/64ee9d4e589bb2cb3788596fd477b0f7/14.8781,120.4546");
            var request = new RestRequest("", Method.GET);
            IRestResponse response = client.Execute(request);

            var content = response.Content;
            var area = JsonConvert.DeserializeObject<WeatherArea>(content);
            lblSummary.Content = area.Currently.Summary;
            lblHumidity.Content = "Humidity: " + area.Currently.Humidity;
            lblWindSpeed.Content = "WindSpeed: " + area.Currently.WindSpeed;
            lblTemperature.Content = "Temperature: " + area.Currently.Temperature;
            lblPressure.Content = "Pressure: " + area.Currently.Pressure;


            BitmapImage wIcon = new BitmapImage();
            wIcon.BeginInit();
            wIcon.UriSource = new Uri("C:\\Users\\VGD pc 2\\Desktop\\weather icons\\" + area.Currently.Icon +".png");
            wIcon.EndInit();
            imgWeatherIcon.Source = wIcon;

        }
    }
}
