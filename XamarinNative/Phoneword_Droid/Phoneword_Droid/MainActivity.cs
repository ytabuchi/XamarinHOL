using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Phoneword_Droid
{
    [Activity(Label = "Phoneword", MainLauncher = true)]
    public class MainActivity : Activity
    {
        static readonly List<string> phoneNumbers = new List<string>();

        private const int DIALOG_ID_CALL = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // ロードされたレイアウトから UI コントロールを取得します。
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            Button callHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);

            // "Call" を Disable にします
            callButton.Enabled = false;
            // 番号を変換するコードを追加します。
            string translatedNumber = string.Empty;
            translateButton.Click += (object sender, EventArgs e) =>
            {
                // ユーザーのアルファベットの電話番号を電話番号に変換します。
                translatedNumber = Core.PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (String.IsNullOrWhiteSpace(translatedNumber))
                {
                    callButton.Text = "Call";
                    callButton.Enabled = false;
                }
                else
                {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;
                }
            };

            callButton.Click += (object sender, EventArgs e) =>
            {
                Bundle args = new Bundle();
                args.PutString("translatedNumber", translatedNumber);
                ShowDialog(DIALOG_ID_CALL, args);
            };

            callHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CallHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };
        }

        protected override Dialog OnCreateDialog(int id, Bundle args)
        {
            switch (id)
            {
                case DIALOG_ID_CALL:
                    {
                        Button callHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);
                        string translatedNumber = args.GetString("translatedNumber");

                        // "Call" ボタンがクリックされたら電話番号へのダイヤルを試みます。
                        var callDialog = new AlertDialog.Builder(this);
                        callDialog.SetMessage("Call " + translatedNumber + "?");
                        callDialog.SetNeutralButton("Call", delegate
                        {
                            // 掛けた番号のリストに番号を追加します。
                            phoneNumbers.Add(translatedNumber);
                            // Call History ボタンを有効にします。
                            callHistoryButton.Enabled = true;
                            // 電話への intent を作成します。
                            var callIntent = new Intent(Intent.ActionCall);
                            callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                            StartActivity(callIntent);
                        });
                        callDialog.SetNegativeButton("Cancel", delegate { });
                        // アラートダイアログを表示し、ユーザーのレスポンスを待ちます。
                        return callDialog.Create();
                    }
                default:
                    break;
            }

            return base.OnCreateDialog(id);
        }
    }
}