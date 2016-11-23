using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            
        }
        public async void ShowMenuList_Clicked(object sender, EventArgs e)
        {
            List<Menu> menus = await AzureManager.AzureManagerInstance.GetMenu();
            MenuList.ItemsSource = menus; 
        }
    }
}
