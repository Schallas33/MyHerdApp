using MyHerdApp.Model;
using MyHerdApp.Pages.MyFarmPage;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyHerdApp.Pages.MyFarmsPage.FarmCampsPage.CampCountPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CampCounterPage : ContentPage
    {
        Camp selectedCamp;
        Farm selectedFarm;

        TimeSpan vibrtime;
        int femalesValue;
        int infantsValue;
        int malesValue;
        string campNotes;
        public bool countPositive = false;

        string savedCampName;

        public CampCounterPage(Farm SelectedFarm, Camp selectedCamp)
        {
            InitializeComponent();

            this.selectedCamp = selectedCamp;
            selectedFarm = SelectedFarm;

            vibrtime = TimeSpan.FromMilliseconds(50);
            savedCampName = selectedCamp.CampName;
            femalesValue = 0;
            infantsValue = 0;
            malesValue = 0;
            campNotes = selectedCamp.Notes;

            campName.Text = savedCampName;
            females.Text = $"Females: {selectedCamp.Females}";
            males.Text = $"Males: {selectedCamp.Males}";
            infants.Text = $"Infants: {selectedCamp.Infants}";
            Notes.Text = campNotes;
            lastCountDate.Text = $" Date last Counted: {selectedCamp.LastCount.ToShortDateString()}";
        }

        private void femalesCount_Unfocused(object sender, FocusEventArgs e)
        {
            if (femalesCount.Text != "" && femalesCount.Text != null)
            {
                femalesValue = Int32.Parse(femalesCount.Text);
            }
            else
            {
                femalesValue = 0;
            }
        }

        public void femalesMinus_Clicked(object sender, EventArgs e)
        {
            if (femalesValue > 0)
            {
                Vibration.Vibrate(vibrtime);
                femalesValue--;
                femalesCount.Text = femalesValue.ToString();
            }
        }

        public void femalesPlus_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(vibrtime);
            femalesValue++;
            femalesCount.Text = femalesValue.ToString();
        }

        private void infantsCount_Unfocused(object sender, FocusEventArgs e)
        {
            if (infantsCount.Text != "" && infantsCount.Text != null)
            {
                infantsValue = Int32.Parse(infantsCount.Text);
            }
            else
            {
                infantsValue = 0;
            }
        }

        public void infantsMinus_Clicked(object sender, EventArgs e)
        {
            if (infantsValue > 0)
            {
                Vibration.Vibrate(vibrtime);
                infantsValue--;
                infantsCount.Text = infantsValue.ToString();
            }
        }

        public void infantsPlus_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(vibrtime);
            infantsValue++;
            infantsCount.Text = infantsValue.ToString();
        }

        private void malesCount_Unfocused(object sender, FocusEventArgs e)
        {
            if (malesCount.Text != "" && malesCount.Text != null)
            {
                malesValue = Int32.Parse(malesCount.Text);
            }
            else
            {
                malesValue = 0;
            }
        }

        public void MalesMinus_Clicked(object sender, EventArgs e)
        {
            if (malesValue > 0)
            {
                Vibration.Vibrate(vibrtime);
                malesValue--;
                malesCount.Text = malesValue.ToString();
            }
        }

        public void malesPlus_Clicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(vibrtime);
            malesValue++;
            malesCount.Text = malesValue.ToString();
        }

        public void Notes_Unfocused(object sender, FocusEventArgs e)
        {
            campNotes = Notes.Text;
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Update Values", "Are you sure you want to Update all values", "Yes", "No");
            if (answer)
            {
                
                selectedCamp.Females = femalesValue;
                selectedCamp.Males = malesValue;
                selectedCamp.Infants = infantsValue;
                selectedCamp.Notes = campNotes;
                selectedCamp.LastCount = DateTime.Today;

                if (femalesValue >= 0 && infantsValue >= 0 && malesValue >= 0)
                {
                    countPositive = true;
                }

                else if (femalesValue < 0)
                {
                    await DisplayAlert("Please Check", "Female Count cannot be less than 0", "OK");
                }
                else if (infantsValue < 0)
                {
                    await DisplayAlert("Please Check", "Infant Count cannot be less than 0", "OK");
                }
                else
                {
                    await DisplayAlert("Please Check", "Male Count cannot be less than 0", "OK");
                }

                if (countPositive)
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Camp>();
                        int rows = conn.Update(selectedCamp);

                        if (rows > 0)
                        {
                            await DisplayAlert("Success", "Camp Updated succesfully", "OK");

                            
                        }
                        else
                        {
                            await DisplayAlert("Caution", "Camp not Updated", "OK");
                        }
                    }

                    await Navigation.PushAsync(new FarmCampsPage(selectedFarm));
                }
            }
        }

        

        


    }
}
    
