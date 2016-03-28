using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Phoneword;
using Phoneword.Droid;

[assembly: ExportRenderer(typeof(RoundedButton), typeof(RoundedButtonRenderer))]
namespace Phoneword.Droid
{
    class RoundedButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundResource(Resource.Drawable.RoundedButton);
            }
        }
    }
}