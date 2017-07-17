using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Livepie
{
    public partial class App : Application
    {
        public const string PoiApiUri = "http://";

        public App(PingPage pingPage, PoiPage poiPage)
        {
            InitializeComponent();

            var carouselPage = new CarouselPage();
            carouselPage.Children.Add(poiPage);
            carouselPage.Children.Add(pingPage);

            MainPage = carouselPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
