using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using System.Threading.Tasks; 

namespace App1
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(); 
    }



    public class App : Application
    {
        public static NavigationPage NavigationPage { get; private set;}
        public static RootPage RootPage; 
        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator; 
        }

        public static bool MenuIsPresented
        {
            get
            {
                return RootPage.IsPresented;
            }
            set
            {
                RootPage.IsPresented = value;
            }
        }

        public App()
        {
            var naviPage = new NaviPage(); 
            NavigationPage = new NavigationPage(new HomePage());
            RootPage = new RootPage();
            RootPage.Master = naviPage;
            RootPage.Detail = NavigationPage;
            MainPage = RootPage; 
            
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
