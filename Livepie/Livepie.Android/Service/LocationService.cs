using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;

namespace Livepie.Droid.Service
{
    public class LocationService
    {
        ILocationListener _locationListener;
        LocationManager _locationManager;

        public LocationService(ILocationListener locationListener, LocationManager locationManager)
        {
            _locationListener = locationListener;
            _locationManager = locationManager;
        }

        public void Pause()
        {
            if (_locationManager != null)
            {
                _locationManager.RemoveUpdates(_locationListener);
            }
        }

        public void Resume()
        {
            var gpsProvider = LocationManager.GpsProvider;

            if (_locationManager != null)
            {
                if (_locationManager.IsProviderEnabled(gpsProvider))
                {
                    _locationManager.RequestLocationUpdates(gpsProvider, 5000, 1, _locationListener as ILocationListener);
                }
            }
        }
    }
}