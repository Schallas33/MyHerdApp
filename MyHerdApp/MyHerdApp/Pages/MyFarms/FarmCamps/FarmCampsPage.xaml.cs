using MyHerdApp.Model;
using MyHerdApp.Pages.MyFarmsPage.FarmCampsPage.CampCountPage;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyHerdApp.Pages.MyFarmsPage.FarmCampsPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FarmCampsPage : ContentPage
    {
        public ObservableCollection<Camp> Items { get; set; }
        public Farm SelectedFarm;

        public FarmCampsPage(Farm SelectedFarm)
        {
            InitializeComponent();
            this.SelectedFarm = SelectedFarm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                
                conn.CreateTable<Farm>();
                conn.CreateTable<Camp>();
                List<Camp> campList = new List<Camp>();
                
                var farmCamps = conn.Table<Camp>().ToList();
                var FarmName = SelectedFarm.FarmName;
                BindingContext = FarmName;

                foreach (var camp in farmCamps)
                {
                    if (camp.FarmID == SelectedFarm.FarmID)
                    {
                        try
                        {
                            campList.Add(camp);
                        }
                        catch (Exception)
                        {

                            return;
                        }
                    }
                }

                FarmCampsListView.ItemsSource = campList;
                int femalesTotal = campList.Select(x => x.Females).Sum();
                int infantsTotal = campList.Select(x => x.Infants).Sum();
                int malesTotal = campList.Select(x => x.Males).Sum();

                List<int> Totals = new List<int>
                {
                    femalesTotal, infantsTotal, malesTotal
                };

                BindingContext = Totals;
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var selectedCamp = e.Item as Camp;
            await Navigation.PushAsync(new CampCounterPage(SelectedFarm, selectedCamp));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            {
                string newCampName = await DisplayPromptAsync("Add a New Camp", "Camp Name:", "OK", "CANCEL");
                if (newCampName == null)
                {
                    await DisplayAlert("Caution", "Camp not Updated", "OK");
                }
                else if (newCampName == "")
                {
                    await DisplayAlert("Caution", "Camp not Updated", "OK");
                }
                else
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Farm>();
                        conn.CreateTable<Camp>();

                        var camp = new Camp
                        {
                            CampName = newCampName,
                            FarmID = SelectedFarm.FarmID,
                        };

                        int rows = conn.Insert(camp);

                        if (rows > 0)
                        {
                            await DisplayAlert("Success", "Camp Added Succesfully", "OK");
                        }

                        SelectedFarm.Camps = new List<Camp> { camp };

                        conn.UpdateWithChildren(camp);
                    }
                }
                OnAppearing();
            }
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Camp compar = (Camp)mi.CommandParameter;
            string newCampName = await DisplayPromptAsync("Change Camp Name", "New Camp Name", "OK", "Cancel");
            compar.CampName = newCampName;
            mi.CommandParameter = compar;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Camp>();

                int rows = conn.Update(compar);
                if (rows > 0)
                {
                    await DisplayAlert("Success", "Camp Updated succesfully", "OK");
                }
                else
                {
                    await DisplayAlert("Caution", "Camp not Updated", "OK");
                }
            }
            OnAppearing();
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var compar = mi.CommandParameter;
            var answer = await DisplayAlert("Delete Camp??", "Are You Sure You Want To Delete This Camp ", "OK", "Cancel");

            if (answer)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Farm>();

                    int rows = conn.Delete(compar);
                    if (rows > 0)
                    {
                        await DisplayAlert("Success", "Camp Deleted Succesfully", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Caution", "Camp not Deleted", "OK");
                    }
                }
                OnAppearing();
            }
        }
    }
}
