using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Count { get; set; } = 0;
        public string ImagePath { get; set; }
        public string Minute { get; set; }
        public string Description { get; set; }

        public dynamic Data { get; set; }
        public dynamic SingleData { get; set; }
        HttpClient httpClient = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        public int Index { get; set; } = 0;
        public void GetMovie()
        {
              var name = movieTextBox.Text;
            HttpResponseMessage response = new HttpResponseMessage();
            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=83a2a211&s={name}&plot=full").Result;

            var str = response.Content.ReadAsStringAsync().Result;
            Data = JsonConvert.DeserializeObject(str);

            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=83a2a211&t={Data.Search[Index].Title}&plot=full").Result;

            str = response.Content.ReadAsStringAsync().Result;
            SingleData = JsonConvert.DeserializeObject(str);

            movieImage.Source = SingleData.Poster;
            movieImage2.Source = SingleData.Poster;
            Minute = SingleData.Runtime;
            Description = SingleData.Genre;

            movieLabel.Content = Minute + " " + Description;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetMovie();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(Index == 0)
            {
                preBtn.Visibility = Visibility.Hidden;
            }
            else
            {
              Index--;
              GetMovie();

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            preBtn.Visibility = Visibility.Visible;
            Index++;
            GetMovie();
        }
    }
}
