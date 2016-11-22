using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;


namespace App1
{
    public partial class TherePage : ContentPage
    {
        public static double start_lat;
        public static double start_long;
        public static double end_lat= -36.783925;
        public static double end_long= 174.754040;

        public TherePage()
        {
            InitializeComponent();
            var map = new Map(
                MapSpan.FromCenterAndRadius(new Position(-36.7837, 174.7530), Distance.FromKilometers(3.0)))
                {
                    IsShowingUser=true,
                    HeightRequest=100,
                    WidthRequest=960,
                    VerticalOptions=LayoutOptions.FillAndExpand,
                    HorizontalOptions=LayoutOptions.Fill
                 };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;

            var cafePosition = new Position(-36.7837, 174.7530);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = cafePosition,
                Label = "The Cafe",
                Address = "74 Taharoto Rd, Takapuna"
            };
            map.Pins.Add(pin);



            

        }

        public async void getUserLocation()    //user location의 GPS데이터 가져오기. 
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var userPosition = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            start_lat = userPosition.Latitude;
            start_long = userPosition.Longitude;
        }


    }
}
