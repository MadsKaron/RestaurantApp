using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Newtonsoft.Json;

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
            //var map = new Map(
            //    MapSpan.FromCenterAndRadius(new Position(-36.7837, 174.7530), Distance.FromKilometers(3.0)))
            //    {
            //        IsShowingUser=true,
            //        HeightRequest=100,
            //        WidthRequest=960,
            //        VerticalOptions=LayoutOptions.FillAndExpand,
            //        HorizontalOptions=LayoutOptions.Fill
            //     };
            //var stack = new StackLayout { Spacing = 0 };
            //stack.Children.Add(map);
            //Content = stack;
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(new Position(-36.7837, 174.7530), Distance.FromKilometers(3.0)));


            var cafePosition = new Position(-36.7837, 174.7530);
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = cafePosition,
                Label = "The Cafe",
                Address = "74 Taharoto Rd, Takapuna"
            };
            MyMap.Pins.Add(pin);

            

        }

        public async void getUserLocation(object sender, EventArgs e)    //user location의 GPS데이터 가져오기. 
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var userPosition = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            start_lat = userPosition.Latitude;
            start_long = userPosition.Longitude;

            RootObject tempUberData = await GetUberJsonAsync(); 
            
            //foreach(UberPrice item in tempUberData.prices){}

            UberList.ItemsSource = tempUberData.prices;

            estimate_distance.Text = tempUberData.prices[0].distance.ToString()+"km"; 
            //await DisplayAlert("is it working?", tempUberData.prices[0].localized_display_name, "OK"); 

        }

        public async Task<RootObject> GetUberJsonAsync()
        {
            string uberServerToken = "bLN5UaKCQ2qTEPZGujfUvWwLMarnpwXoLPJUXQ0I";

            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("https://api.uber.com/v1.2/estimates/price");
            string allParams = "?server_token=" + uberServerToken + "&start_latitude=" + start_lat.ToString()
                + "&start_longitude=" + start_long.ToString() + "&end_latitude=" + end_lat.ToString() + "&end_longitude=" + end_long.ToString();
            var response = await client.GetStringAsync(allParams);

            RootObject uberData = JsonConvert.DeserializeObject<RootObject>(response);

            return uberData; 
        }



    }
}
