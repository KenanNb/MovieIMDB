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
        public string Rating { get; set; }
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
            Rating = SingleData.imdbRating;
            double rating = double.Parse(Rating);
            if(rating != 0)
            {
                star0.Visibility = Visibility.Collapsed;
                star01.Visibility = Visibility.Collapsed;
                star02.Visibility = Visibility.Collapsed;
                star03.Visibility = Visibility.Collapsed;
                star04.Visibility = Visibility.Collapsed;
            }
            if(rating > 0.0 && rating < 2.0)
            {
                starHalf.Visibility = Visibility.Visible;
            }
            if(rating == 2)
            {
                starHalf.Visibility = Visibility.Collapsed;
                star1.Visibility = Visibility.Visible;
            }
            if (rating > 2.0 && rating < 4.0)
            {
                starHalf.Visibility = Visibility.Collapsed;
                star1.Visibility = Visibility.Visible;
                starHalf1.Visibility = Visibility.Visible;
            }
            if (rating == 4)
            {
                starHalf.Visibility = Visibility.Collapsed;
                star1.Visibility = Visibility.Visible;
                starHalf1.Visibility = Visibility.Collapsed;
                star2.Visibility = Visibility.Visible;
            }
            if (rating > 4.0 && rating < 6.0)
            {
                starHalf.Visibility = Visibility.Collapsed;
                star1.Visibility = Visibility.Visible;
                starHalf1.Visibility = Visibility.Collapsed;
                star2.Visibility = Visibility.Visible;
                starHalf2.Visibility = Visibility.Visible;
            }
            if (rating == 6)
            {
                starHalf.Visibility = Visibility.Collapsed;
                star1.Visibility = Visibility.Visible;
                starHalf1.Visibility = Visibility.Collapsed;
                star2.Visibility = Visibility.Visible;
                starHalf2.Visibility = Visibility.Collapsed;
                
            }
            if (rating > 6.0 && rating < 8.0)
            {
                starHalf.Visibility = Visibility.Collapsed;
                star1.Visibility = Visibility.Visible;
                starHalf1.Visibility = Visibility.Collapsed;
                star2.Visibility = Visibility.Visible;
                starHalf2.Visibility = Visibility.Collapsed;
                star3.Visibility = Visibility.Visible;
                starHalf3.Visibility = Visibility.Visible;
            }
            if (rating == 8)
            {
                starHalf.Visibility = Visibility.Collapsed;
                star1.Visibility = Visibility.Visible;
                starHalf1.Visibility = Visibility.Collapsed;
                star2.Visibility = Visibility.Visible;
                starHalf3.Visibility = Visibility.Collapsed;
                star4.Visibility = Visibility.Visible;
            }
            if (rating > 8.0 && rating < 10.0)
            {
                starHalf.Visibility = Visibility.Collapsed;
                star1.Visibility = Visibility.Visible;
                starHalf1.Visibility = Visibility.Collapsed;
                star2.Visibility = Visibility.Visible;
                starHalf2.Visibility = Visibility.Collapsed;
                star3.Visibility = Visibility.Visible;
                starHalf3.Visibility = Visibility.Collapsed;
                star4.Visibility = Visibility.Visible;
                starHalf4.Visibility = Visibility.Visible;
            }
            if (rating == 10)
            {
                starHalf.Visibility = Visibility.Collapsed;
                star1.Visibility = Visibility.Visible;
                starHalf1.Visibility = Visibility.Collapsed;
                star2.Visibility = Visibility.Visible;
                starHalf3.Visibility = Visibility.Collapsed;
                star4.Visibility = Visibility.Visible;
                starHalf4.Visibility = Visibility.Collapsed;
                star5.Visibility = Visibility.Visible;
            }            
            RatingLbl1.Content = "Rating";
            RatingLbl2.Content = Rating;
            PlotTxtblock.Text = SingleData.Plot;
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
            try
            {
              preBtn.Visibility = Visibility.Visible;
              Index++;
              GetMovie();             
            }
            catch (Exception)
            {
                nextBtn.Visibility = Visibility.Hidden;
                --Index;
            }
        }
    }
}
