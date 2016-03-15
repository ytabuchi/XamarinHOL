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
            MainPage = new NavigationPage(new Phoneword.MainPage());
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
