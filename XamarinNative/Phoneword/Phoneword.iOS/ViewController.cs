using System;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace Phoneword.iOS
{
    public partial class ViewController : UIViewController
    {
        // translatedNumberをViewDidLoad()から移動します
        string translatedNumber = "";
        public List<string> PhoneNumbers { get; set; }

        public ViewController(IntPtr handle) : base(handle)
        {
            // Call History画面用に電話番号のListを初期化します
            PhoneNumbers = new List<string>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            TranslateButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                // PhoneTranslator.csを使用してテキストから電話番号に変換します
                translatedNumber = Core.PhonewordTranslator.ToNumber(PhoneNumberText.Text);
                // TextFieldがタップされたらキーボードをDismissします
                PhoneNumberText.ResignFirstResponder();

                if (translatedNumber == "")
                {
                    CallButton.SetTitle("Call", UIControlState.Normal);
                    CallButton.Enabled = false;
                }
                else
                {
                    CallButton.SetTitle("Call " + translatedNumber, UIControlState.Normal);
                    CallButton.Enabled = true;
                }
            };

            CallButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                // 変換した電話番号をPhoneNumbersに追加します
                PhoneNumbers.Add(translatedNumber);

                var url = new NSUrl("tel:" + translatedNumber);
                // 標準の電話アプリを呼び出すためにtel:のプリフィックスでURLハンドラーを使用します
                // できない場合はAlertViewを呼び出します。
                if (!UIApplication.SharedApplication.OpenUrl(url))
                {
                    var alert = UIAlertController.Create("Not supported", "Scheme 'tel:' is not supported on this device", UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                    PresentViewController(alert, true, null);
                }
            };
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            // set the View Controller that’s powering the screen we’re
            // transitioning to
            var callHistoryContoller = segue.DestinationViewController as CallHistoryController;
            //set the Table View Controller’s list of phone numbers to the
            // list of dialed phone numbers
            if (callHistoryContoller != null)
            {
                callHistoryContoller.PhoneNumbers = PhoneNumbers;
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

