using Livepie.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Timers;

namespace Livepie
{
    public class PingPage : ContentPage
    {
        Label _labelLeft;
        Label _labelRight;
        LocationService _locationService;
        Timer _timer;

        public PingPage()
        {
            _locationService = new Services.LocationService(500, 5000);
            _locationService.OnLocationChanged += LocationService_OnLocationChanged;

            _labelLeft = new Label()
            {
                FontSize = 20,
            };

            _labelRight = new Label()
            {
                FontSize = 20,
            };

            var labelBroadcast = new Label()
            {
                FontSize = 20,
                Text = "Broadcasting Location"
            };

            var stackLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            stackLayout.Children.Add(_labelLeft);
            stackLayout.Children.Add(labelBroadcast);
            stackLayout.Children.Add(_labelRight);

            Content = stackLayout;

            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _locationService.Resume();

            _timer.Start();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _locationService.Pause();

            _timer.Stop();
        }

        void LocationService_OnLocationChanged(LocationChangedEventArgs e)
        {

        }

        int _counter = 0;

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var textLeft = string.Empty;
            var textRight = string.Empty;

            switch (_counter)
            {
                case 0:
                    textLeft = string.Empty;
                    textRight = string.Empty;
                    _counter++;
                    break;
                case 1:
                    textLeft = "(";
                    textRight = ")";
                    _counter++;
                    break;
                case 2:
                    textLeft = "((";
                    textRight = "))";
                    _counter++;
                    break;
                case 3:
                    textLeft = "(((";
                    textRight = ")))";
                    _counter = 0;
                    break;
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                _labelLeft.Text = textLeft;
                _labelRight.Text = textRight;
            });
        }
    }
}