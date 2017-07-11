using System;
using Xamarin.Forms;

namespace XF_Phoneword
{
    public class CallHistoryPage : ContentPage
    {
        public CallHistoryPage()
        {
            Title = "Recent Call";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = {
                    new ListView
                    {
                        ItemsSource = App.PhoneNumbers,
                    }
                }
            };
        }
    }
}
