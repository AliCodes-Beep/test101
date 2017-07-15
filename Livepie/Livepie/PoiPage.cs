using Livepie.Services;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Livepie
{
    public class PoiPage : ContentPage
    {
        double _latitude, _longitude;
        LocationService _locationService;
        Map _map;

        public PoiPage()
        {
            _locationService = new Services.LocationService(500, 5000);
            _locationService.OnLocationChanged += LocationService_OnLocationChanged;

            _map = new Map()
            {
                HasScrollEnabled = false,
                HeightRequest = 100,
                IsShowingUser = true,
                MapType = MapType.Hybrid,
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = 960
            };

            _map.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "VisibleRegion" && _map.VisibleRegion != null)
                {
                    CalculateBoundingBox(_map.VisibleRegion);
                }
            };

            var stackLayout = new StackLayout()
            {
                Spacing = 0
            };

            stackLayout.Children.Add(_map);

            Content = stackLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _locationService.Resume();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _locationService.Pause();
        }

        // http://forums.xamarin.com/discussion/22493/maps-visibleregion-bounds
        void CalculateBoundingBox(MapSpan region)
        {
            var center = region.Center;
            var halfHeightDegrees = region.LatitudeDegrees / 2;
            var halfWidthDegrees = region.LongitudeDegrees / 2;

            var left = center.Longitude - halfWidthDegrees;
            var right = center.Longitude + halfWidthDegrees;
            var top = center.Latitude + halfHeightDegrees;
            var bottom = center.Latitude - halfHeightDegrees;

            if (left < -180) left = 180 + (180 + left);
            if (right > 180) right = (right - 180) - 180;
        }

        void LocationService_OnLocationChanged(LocationChangedEventArgs e)
        {
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(e.Latitude, e.Longitude), Distance.FromKilometers(1)));
        }
    }
}