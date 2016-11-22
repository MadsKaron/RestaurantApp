using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new MenuPageViewModel();
        }
        /*
        async void GoThereButtonClicked(object sender, EventArgs args)
        {

        }
        async void MenuButtonClicked(object sender, EventArgs args)
        {

        }
        async void MemberButtonClicked(object sender, EventArgs args)
        {

        }*/
    }
}
