using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace Livepie.Services
{
    public class LocationService
    {
        public delegate void LocationChanged(LocationChangedEventArgs e);
        public event LocationChanged OnLocationChanged;

        double _minimumDistance;
        int _minimumTime;
        

        IGeolocator _crossGeolocator;

        public LocationService(double minimumDistance, int minimumTime)
        {
            _minimumDistance = minimumDistance;
            _minimumTime = minimumTime;

            _crossGeolocator = CrossGeolocator.Current;
            _crossGeolocator.PositionChanged += _crossGeolocator_PositionChanged;
        }

        public void Pause()
        {
            if (_crossGeolocator != null)
            {
                _crossGeolocator.StopListeningAsync();
            }
        }

        public void Resume()
        {
            if (_crossGeolocator != null)
            {
                _crossGeolocator.StartListeningAsync(minTime: _minimumTime, minDistance: _minimumDistance);
            }
        }

        void _crossGeolocator_PositionChanged(object sender, PositionEventArgs e)
        {
            OnLocationChanged?.Invoke(new LocationChangedEventArgs()
            {
                Latitude = e.Position.Latitude,
                Longitude = e.Position.Longitude
            });
        }
    }
}
