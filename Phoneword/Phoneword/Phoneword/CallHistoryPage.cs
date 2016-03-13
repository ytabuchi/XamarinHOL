using System;
using Xamarin.Forms;

namespace Phoneword
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
                Children =
                {
                    new ListView
                    {
                        ItemsSource = App.PhoneNumbers,
                        ItemTemplate = new DataTemplate(typeof(CustomCell)),
                        HasUnevenRows = true,
                    }
                }
            };
        }
    }

    /// <summary>
    /// ViewCell を継承した Custom DataTemplate
    /// </summary>
    public class CustomCell : ViewCell
    {
        public CustomCell()
        {
            // 変換後の電話番号のLabel
            var translatedLabel = new Label
            {
                Style = Device.Styles.TitleStyle
            };
            translatedLabel.SetBinding(Label.TextProperty, "TranslatedNumber");

            // 変換前の電話番号のLabel
            var rawLabel = new Label
            {
                FontSize = 10,
                TextColor = Color.Navy,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            rawLabel.SetBinding(Label.TextProperty, "RawNumber");

            // 電話を書けた日時のLabel
            var dateLabel = new Label
            {
                FontSize = 9,
                TextColor = Color.FromHex("666666"),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.End,
            };
            dateLabel.SetBinding(Label.TextProperty,
                new Binding("CallDate", stringFormat: "{0:yyyy年MM月dd日 hh時mm分}"));

            View = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 5,
                Children =
                {
                    translatedLabel,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {

                            rawLabel,
                            dateLabel
                        }
                    }
                },
            };
        }
    }
}