using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyHerdApp.Pages.JustCountPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JustCount : ContentPage
    {
        public JustCount()
        {
            InitializeComponent();
        }

        public int femalesValue = 0;
        public int infantsValue = 0;
        public int malesValue = 0;
        TimeSpan vibrtime = TimeSpan.FromMilliseconds(50);

        public void femalesMinus_Clicked(object sender, EventArgs e)
        {
            if (femalesValue > 0)
            {
                femalesValue--;
                femalesCount.Text = femalesValue.ToString();
                Vibration.Vibrate(vibrtime);
            }
        }


        private void femalesPlus_Clicked(object sender, EventArgs e)
        {
            femalesValue++;
            femalesCount.Text = femalesValue.ToString();
            Vibration.Vibrate(vibrtime);
        }

        private void infantsMinus_Clicked(object sender, EventArgs e)
        {
            if (infantsValue > 0)
            {
                infantsValue--;
                infantsCount.Text = infantsValue.ToString();
            }
        }

        private void infantsPlus_Clicked(object sender, EventArgs e)
        {
            infantsValue++;
            infantsCount.Text = infantsValue.ToString();
            Vibration.Vibrate(vibrtime);
        }

        private void MalesMinus_Clicked(object sender, EventArgs e)
        {
            if (malesValue > 0)
            {
                malesValue--;
                malesCount.Text = malesValue.ToString();
                Vibration.Vibrate(vibrtime);
            }
        }

        private void malesPlus_Clicked(object sender, EventArgs e)
        {
            malesValue++;
            malesCount.Text = malesValue.ToString();
            Vibration.Vibrate(vibrtime);
        }

        private void resetButton_Clicked(object sender, EventArgs e)
        {
            femalesValue = 0;
            femalesCount.Text = femalesValue.ToString();
            infantsValue = 0;
            infantsCount.Text = infantsValue.ToString();
            malesValue = 0;
            malesCount.Text = malesValue.ToString();
            Vibration.Vibrate(TimeSpan.FromSeconds(1));
        }
    }
}