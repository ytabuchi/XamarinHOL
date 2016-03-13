using Windows.ApplicationModel.Calls;
using Phoneword.WinPhone;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]

namespace Phoneword.WinPhone
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            PhoneCallManager.ShowPhoneCallUI(number, "Phoneword");
            return true;
        }
    }
}