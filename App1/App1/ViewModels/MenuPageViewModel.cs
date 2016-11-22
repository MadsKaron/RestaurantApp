
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1
{
    public class MenuPageViewModel 
    {
        public ICommand GoHomeCommand{ get; set; } //go home page
        public ICommand GoMemberCommand { get; set; } //Member page
        public ICommand GoMenuCommand { get; set; } //Menu page
        public ICommand GoToThereCommand { get; set; } // To trasport page


        public MenuPageViewModel()
        {
            GoHomeCommand = new Command(GoHome);
            GoMemberCommand = new Command(GoMember);
            GoMenuCommand = new Command(GoMenu);
            GoToThereCommand = new Command(GoThere); 
        }

        void GoHome(Object obj)
        {
            App.RootPage.Detail = new NavigationPage(new HomePage());
            App.MenuIsPresented = false; 
        }

        void GoMember(Object obj)
        {
            App.RootPage.Detail = new NavigationPage(new MemberPage());
            App.MenuIsPresented = false;
        }

        void GoMenu(Object obj)
        {
            App.RootPage.Detail = new NavigationPage(new MenuPage());
            App.MenuIsPresented = false;
        }

        void GoThere(Object obj)
        {
            App.RootPage.Detail = new NavigationPage(new TherePage());
            App.MenuIsPresented = false;
        }
    }
}
