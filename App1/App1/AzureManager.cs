using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<Menu> menuTable; 

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://thisishailiey.azurewebsites.net/");
            this.menuTable = this.client.GetTable<Menu>(); 
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task<List<Menu>> GetMenu()
        {
            return await this.menuTable.ToListAsync(); 
        }
    }
}
