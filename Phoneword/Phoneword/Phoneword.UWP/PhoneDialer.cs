using Windows.ApplicationModel.Calls;
using Phoneword.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialer))]

namespace Phoneword.UWP
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