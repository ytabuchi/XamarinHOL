using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        string translatedNumber;

        public MainPage()
        {
            InitializeComponent();
        }

        void OnTranslate(object sender, EventArgs e)
        {
            translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
            if (!string.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            await this.DisplayAlert(
                    "Dial a Number",
                    "Would you like to call " + translatedNumber + "?",
                    "Yes",
                    "No");

            var status = PermissionStatus.Unknown;
            // Permissionを取得しているかを確認
            status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);

            // 持っていなかったらPermissionをリクエスト
            if (status != PermissionStatus.Granted)
                status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone))[Permission.Phone];

            // Permissionを取得できたらIntentに投げる
            if (status == PermissionStatus.Granted)
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    //dialer.Dial(translatedNumber);
                    App.PhoneNumbers.Add(new Numbers(phoneNumberText.Text, translatedNumber, DateTime.Now));
                    callHistoryButton.IsEnabled = true;
                    dialer.Dial(translatedNumber);
                }

            }

            //if (await this.DisplayAlert(
            //        "Dial a Number",
            //        "Would you like to call " + translatedNumber + "?",
            //        "Yes",
            //        "No"))
            //{
            //    var dialer = DependencyService.Get<IDialer>();
            //    if (dialer != null)
            //    {
            //        //App.PhoneNumbers.Add(translatedNumber);
            //        App.PhoneNumbers.Add(new Numbers(phoneNumberText.Text, translatedNumber, DateTime.Now));
            //        callHistoryButton.IsEnabled = true;
            //        dialer.Dial(translatedNumber);
            //    }
            //}
        }

        async void OnCallHistory(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new CallHistoryPage());
            await Navigation.PushAsync(new CallHistoryPageXaml());
        }

    }
}