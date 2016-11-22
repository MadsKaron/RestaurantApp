using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class NaviPage : ContentPage
    {
        public NaviPage()
        {
            InitializeComponent();
            BindingContext = new MenuPageViewModel();
            Title = "Navi";
            Icon = Device.OS == TargetPlatform.iOS ? "menu.png" : null;
            
        }
    }
}
