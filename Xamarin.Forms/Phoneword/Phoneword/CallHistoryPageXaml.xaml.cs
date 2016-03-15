using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Phoneword
{
    public partial class CallHistoryPageXaml : ContentPage
    {
        public CallHistoryPageXaml()
        {
            InitializeComponent();
            this.BindingContext = App.PhoneNumbers;
        }
    }
}
