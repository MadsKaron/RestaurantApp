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
        private IMobileServiceTable<Members> memberTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://thisishailiey.azurewebsites.net/");
            this.menuTable = this.client.GetTable<Menu>();
            this.memberTable = this.client.GetTable<Members>(); 
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

        public async Task<List<Members>> GetMember()
        {
            return await this.memberTable.ToListAsync(); 
        }

        public async Task<List<Members>> GetAndCheckUser(string email)
        {
            return await memberTable.Where(memberTable => memberTable.email_address.Equals(email)).ToListAsync();
        }

        public async Task AddMember(Members member)
        {
            await this.memberTable.InsertAsync(member); 
        }

    }
}
