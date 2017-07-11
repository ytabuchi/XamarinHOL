using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using Xamarin.Forms;

namespace XF_Phoneword
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
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            // Alertの選択をboolで取得します。
            var call = await DisplayAlert(
              "Dial a Number",
              "Would you like to call " + translatedNumber + "?",
              "Yes",
              "No");
            if (call)
            {
                try
                {
                    // PermissionPluginを使用してPhoneのPermissionのStatusを取得します。
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);

                    // Permission未設定の場合は確認ダイアログを表示しPermissionの取得を確認します。
                    if (status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Phone))
                        {
                            await DisplayAlert("Need Permission", "It will get permission of phonecall", "OK");
                        }

                        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Phone });
                        status = results[Permission.Phone];
                    }

                    // Permissionが許可されていれば電話を掛けます。
                    if (status == PermissionStatus.Granted)
                    {
                        var dialer = DependencyService.Get<IDialer>();
                        if (dialer != null)
                        {
                            App.PhoneNumbers.Add(translatedNumber);
                            callHistoryButton.IsEnabled = true;
                            dialer.Dial(translatedNumber);
                        }
                    }
                    else if (status != PermissionStatus.Unknown)
                    {
                        await DisplayAlert("Permission Denied", "Can not continue, try again.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.InnerException);
                }
            }

        }
    }
}