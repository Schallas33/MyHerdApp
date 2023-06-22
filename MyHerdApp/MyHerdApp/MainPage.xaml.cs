using MyHerdApp.Pages.JustCountPage;
using MyHerdApp.Pages.MyFarmPage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyHerdApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var assembly = typeof(MainPage);
            Logo.Source = ImageSource.FromResource("MyHerdApp.Resources.Images.Bull.png", assembly);
        }

        private void countButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new JustCount());
        }

        private void myFarms_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyFarmsPage());
        }

        private void myEvents_Clicked(object sender, EventArgs e)
        {

        }
    }
}
