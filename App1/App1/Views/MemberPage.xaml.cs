using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.Settings;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using System.Net.Http;
using Xamarin.Auth;
using Newtonsoft.Json.Linq;

namespace App1
{
    public partial class MemberPage : ContentPage
    {
        static Members newMember = new Members();
        static Members aMember = new Members();
        static Button registered = new Button { Text = "Have an account" };
        static Button newUser = new Button { Text = "Create an account" };
        static Button login = new Button { Text = "Login", IsVisible=false };
        static Button submit = new Button { Text = "Create", IsVisible = false };
        static EntryCell username = new EntryCell { Label = "Username", Placeholder = "Perhaps your email address:)", Keyboard=Keyboard.Email};
        static EntryCell password = new EntryCell { Label = "Password", Placeholder = "Enter your password :-)" };
        static EntryCell nameToRegister = new EntryCell { Label = "Name", Placeholder = "Enter your full name :p" };
        static TableView tableView;
        static TableView registerTableView;
        static ActivityIndicator actIndicator = new ActivityIndicator { Color = Color.Pink, HorizontalOptions = LayoutOptions.Center, IsVisible = false, IsRunning = true };
        static StackLayout lay = new StackLayout { };
        static Button logout = new Button { Text = "Logout" };
        static Members theMember = new Members();

        public MemberPage()
        {
            //InitializeComponent();
            //username.Completed += Username_Completed;
            //password.Completed += Password_Completed; 

            var layout = new StackLayout { Padding = 10 };
            this.Content = new StackLayout
            {
                Children =
                {
                    registered,
                    newUser,
                    layout,
                    actIndicator
                }
            };
            

            registered.Clicked += HaveAccount_Clicked;
            newUser.Clicked += NewUser_Clicked;

            tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        username,
                        password
                    }
                }
            }; // for login

            registerTableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot
                {
                    new TableSection
                    {
                        username,
                        password,
                        nameToRegister
                    }
                }
            }; // for create account 

            login.Clicked += LoginButton_Clicked;
            submit.Clicked += Submit_Clicked;
        }

        
        private void NewUser_Clicked(object sender, EventArgs e)
        {
            lay = new StackLayout { };
            
            Content = new StackLayout
            {
                Children =
                {
                    registerTableView,
                    submit
                }
            };
            registered.IsVisible = false;
            newUser.IsVisible = false;
            submit.IsVisible = true;
            submit.IsEnabled = true; 
        }

        public async void HaveAccount_Clicked(object sender, EventArgs args)
        {
            lay = new StackLayout();
            Content = new StackLayout
            {
                Children =
                {
                    tableView,
                    login
                }
            }; //showing the login form
            registered.IsVisible = false;
            newUser.IsVisible = false;
            login.IsVisible = true; 
            login.IsEnabled = true;
        } 


        public async void LoginButton_Clicked(object sender, EventArgs args)
        {
            lay = new StackLayout { };

            actIndicator.IsVisible = true;
            aMember.email_address = username.Text;
            aMember.password = password.Text;

            List<Members> aList = await AzureManager.AzureManagerInstance.GetAndCheckUser(aMember.email_address.ToString());
            actIndicator.IsVisible = false;

            if (aList.Count == 0)
            {
                await DisplayAlert("Login failed","Please check your ID and password again", "OK");
            }else
            {
               int count= aList.Where(Members => Members.password==aMember.password).Count<Members>();

                if (count == 1)
                {
                    await DisplayAlert("Login Success! Welcome", aList.Where(Members => Members.email_address==aMember.email_address).FirstOrDefault().name, "OK");

                     theMember = aList.Where(Members => Members.email_address == aMember.email_address).First(); 

                    
                    this.Content = lay;

                    var greetingLabel = new Label { HorizontalTextAlignment=TextAlignment.Center, FontSize=Device.GetNamedSize(NamedSize.Large,typeof(Label))};
                    greetingLabel.Text = String.Format(
                        "Hello, {0} \n Your latest visit was {1}. \n Your current point is {2}. \n Cheers! \n"
                        ,theMember.name, theMember.latest_visit, theMember.user_point);
                    lay.Children.Add(greetingLabel);
                    lay.Children.Add(logout);
                    logout.Clicked += Logout_Clicked1;
                    

                }
                else
                {
                    await DisplayAlert("Login failed", "Please check your ID and password again", "OK");
                    //InitializeComponent();
                }
            }
            
        }

        private async void Logout_Clicked1(object sender, EventArgs e)
        {
            if (DateTime.Today > theMember.updatedAt.Date)
            {
                theMember.user_point += 1;
                theMember.latest_visit = DateTime.Today;
            }

            await AzureManager.AzureManagerInstance.UpdateMemberLastVisit(theMember);

           
            lay.IsVisible = false;
            lay = new StackLayout { };
            registered.IsVisible = true;
            newUser.IsVisible = true;
            lay.Children.Add(registered);
            lay.Children.Add(newUser);
            //InitializeComponent();
            ResetValues();
           

        }


        private async void Submit_Clicked(object sender, EventArgs e)
        {
            lay = new StackLayout { };
            newMember.email_address = username.Text;
            newMember.password = password.Text;
            newMember.name = nameToRegister.Text;
            newMember.user_point = 0;
            newMember.latest_visit = DateTime.Today;

            await AzureManager.AzureManagerInstance.AddMember(newMember);

            
            //ResetValues();
            
            await DisplayAlert("Result", "Great! Happy to be your beloved De Caffe", "OK");
            ///
            this.Content = lay;
            lay.IsVisible = false;
            lay = new StackLayout { };
            registered.IsVisible = true;
            newUser.IsVisible = true;
            lay.Children.Add(registered);
            lay.Children.Add(newUser);
            ResetValues();
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    ResetValues();

        //}
        public void ResetValues()
        {
            username.Text = null;
            password.Text = null;
            nameToRegister.Text = null;
            aMember = new Members();
            theMember = new Members();
            newMember = new Members();
        }


















            //protected override async void OnAppearing()
            //{
            //    base.OnAppearing();

            //    userId = CrossSettings.Current.GetValueOrDefault("user", "");
            //    string token = CrossSettings.Current.GetValueOrDefault("token", "");


            //    if (!token.Equals("") && !userId.Equals(""))
            //    {
            //        MobileServiceUser user = new MobileServiceUser(userId);
            //        user.MobileServiceAuthenticationToken = token;

            //        AzureManager.AzureManagerInstance.AzureClient.CurrentUser = user;

            //        authenticated = true;

            //    }

            //    if (authenticated == true)
            //    {
            //        loginButton.IsVisible = false;
            //        //Loginn.Text= "It is login-ed!!!";
            //        //user_id.Text = userId;
            //        logoutButton.IsVisible = true;

            //        //await DisplayAlert("About user ID", AzureManager.AzureManagerInstance.AzureClient.CurrentUser.UserId, "OK");
            //        var tokenForUser = App.Authenticator.Authenticate().Result.ToString();
            //        await DisplayAlert("Result: ", tokenForUser, "OK");
            //    }
            //}

            //async void loginButton_Clicked(object obj, EventArgs e)
            //{
            //    if (App.Authenticator != null)
            //        authenticated = await App.Authenticator.Authenticate();

            //    if (authenticated == true)
            //    {
            //        loginButton.IsVisible = false;
            //        CrossSettings.Current.AddOrUpdateValue("user", AzureManager.AzureManagerInstance.AzureClient.CurrentUser.UserId);
            //        CrossSettings.Current.AddOrUpdateValue("token", AzureManager.AzureManagerInstance.AzureClient.CurrentUser.MobileServiceAuthenticationToken);
            //        userId = AzureManager.AzureManagerInstance.AzureClient.CurrentUser.UserId;


            //    } //여기까지는 잘되는 부분



            //}

            //async void logoutButton_Clicked(object sender, EventArgs e)
            //{
            //    bool loggedOut = false;
            //    if (App.Authenticator != null)
            //    {
            //        loggedOut = await App.Authenticator.LogoutAsync();
            //        //await AzureManager.AzureManagerInstance.AzureClient.LogoutAsync();
            //    }

            //}



        }
}
