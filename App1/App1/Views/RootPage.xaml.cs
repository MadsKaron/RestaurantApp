using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App1
{
    public partial class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;

        }

        public bool DoBack
        {
            get
            {
                MasterDetailPage mainP = App.Current.MainPage as MasterDetailPage; 

                if(mainP !=null)
                {
                    bool canDoBack = mainP.Detail.Navigation.NavigationStack.Count > 1 || mainP.IsPresented; 

                    if(!canDoBack)
                    {
                        mainP.IsPresented = true;
                        return false; 
                    }
                    else { return true; }  //여기를 어떻게 좀 하면 뒤로가기 버튼 눌렀을때 메인페이지로 가지 않을까...?
                }
                return true; 
            }

        }
    }
}
