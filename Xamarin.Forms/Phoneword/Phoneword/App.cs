using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace Phoneword
{
    public class App : Application
    {
        //public static List<string> PhoneNumbers { get; set; }
        public static List<Numbers> PhoneNumbers { get; set; }

        public App()
        {
            //PhoneNumbers = new List<string>();
            PhoneNumbers = new List<Numbers>();
            var nav = new NavigationPage(new Phoneword.MainPage());
            nav.BarBackgroundColor = Color.FromHex("3498DB");
            nav.BarTextColor = Color.White;
            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
