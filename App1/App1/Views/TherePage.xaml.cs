using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace App1
{
    public partial class TherePage : ContentPage
    {

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


    }
}
