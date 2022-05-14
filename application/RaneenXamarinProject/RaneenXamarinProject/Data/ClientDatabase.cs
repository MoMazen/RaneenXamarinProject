using RaneenXamarinProject.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RaneenXamarinProject.Data
{
    public class ClientDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ClientDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ClientData>().Wait();
        }

        public Task<List<ClientData>> GetAllClientsDataAsync()
        {
            return database.Table<ClientData>().ToListAsync();
        }

        public Task<ClientData> GetClientDataAsync(string clientToken)
        {
            return database.Table<ClientData>()
                .Where(client => client.Token == clientToken)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveClientDataAsync(ClientData clientData)
        {
            var client = database.Table<ClientData>()
                .Where(c => c.Token == clientData.Token).FirstOrDefaultAsync().Result;

            if (client == null)
            {
                // save new client
                return database.InsertAsync(clientData);
            }
            else
            {
                // on updating cartItems
                if(clientData.wishList == null)
                {
                    clientData.wishList = client.wishList;
                }
                // on updating wishList
                if (clientData.cartItems == null)
                {
                    clientData.cartItems = client.cartItems;
                }

                // update existing client
                return database.UpdateAsync(clientData);
            }
        }

        public Task<int> DeleteClientDataAsync(ClientData clientData)
        {
            // Delete a note.
            return database.DeleteAsync(clientData);
        }
    }
}
