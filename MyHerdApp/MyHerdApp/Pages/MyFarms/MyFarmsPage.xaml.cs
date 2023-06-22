using MyHerdApp.Model;
using SQLiteNetExtensions.Attributes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyHerdApp.Pages.MyFarmsPage.FarmCampsPage;

namespace MyHerdApp.Pages.MyFarmPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFarmsPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        

        public MyFarmsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Farm>();
                var Items = conn.Table<Farm>().ToList();
                MyFarmsListView.ItemsSource = Items;
            }
        }

            public void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var selectedFarm = e.Item as Farm;
            Navigation.PushAsync(new FarmCampsPage(selectedFarm));
        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string newFarmName = await DisplayPromptAsync("Add a New Farm", "Farm Name:","OK","CANCEL");

            if (newFarmName == null)
            {
                await DisplayAlert("Caution", "Farm not Updated", "OK");
            }
            else if (newFarmName == "")
            {
                await DisplayAlert("Caution", "Farm not Updated", "OK");
            }
            else
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    Farm farm = new Farm();
                    farm.FarmName = newFarmName;

                    conn.CreateTable<Farm>();
                    int rows = conn.Insert(farm);

                    if (rows > 0)
                    {
                        await DisplayAlert("Success", "Farm Added Succesfully", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Caution", "Farm not Added", "OK");
                    }
                }
            }
            OnAppearing();
        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Farm compar = (Farm)mi.CommandParameter;
            string newFarmName = await DisplayPromptAsync("Change Farm Name", "New Farm Name", "OK", "Cancel");
            compar.FarmName = newFarmName;
            mi.CommandParameter = compar;

            using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Farm>();
                
                int rows = conn.Update(compar);
                if (rows > 0)
                {
                    await DisplayAlert("Success", "Farm Updated succesfully", "OK");
                }
                else
                {
                    await DisplayAlert("Caution", "Farm not Updated", "OK");
                }
            }
            OnAppearing();
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var compar = mi.CommandParameter;
            var answer = await DisplayAlert("Delete Farm??","Are You Sure You Want To Delete This Farm ","OK", "Cancel");

            if (answer)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Farm>();

                    int rows = conn.Delete(compar);
                    if (rows > 0)
                    {
                        await DisplayAlert("Success", "Farm Deleted Succesfully", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Caution", "Farm not Deleted", "OK");
                    }
                }
                OnAppearing();
            }
        }
    }
    }

