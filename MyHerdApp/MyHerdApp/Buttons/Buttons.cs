using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MyHerdApp.Buttons
{
    class Buttons
    {
        TimeSpan vibrtime;
        int femalesValue;
        int infantsValue;
        int malesValue;

        
        public void Vibrate(int time)
        {
            vibrtime = TimeSpan.FromMilliseconds(time);
            Vibration.Vibrate(vibrtime);
        }
        
        public void FemaleMinusButton()
        {
            if (femalesValue > 0)
            {
                Vibrate(50);
                femalesValue--;
            }
        }

        public void FemalePlusButton()
        {
            Vibrate(50);
            femalesValue++;
        }

        public void InfantMinusButton()
        {
            if (infantsValue > 0)
            {
                Vibrate(50);
                infantsValue--;
            }
        }

        public void InfantPlusButton()
        {
            Vibrate(50);
            infantsValue++;
        }

        public void MalesMinusButton()
        {
            if (malesValue > 0)
            {
                Vibrate(50);
                malesValue--;
            }
        }

        public void MalesPlusButton()
        {
            Vibrate(50);
            malesValue++;
        }
    }
}
