using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Phoneword;
using Phoneword.UWP;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(RoundedButtonRenderer))]
namespace Phoneword.UWP
{
    class RoundedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            var customTemplate = App.Current.Resources["RoundedButton"] as Windows.UI.Xaml.Controls.ControlTemplate;
            if (Control != null)
            {
                Control.Template = customTemplate;
            }
        }
    }
}
