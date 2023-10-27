
using Essensgeldkasse.Models;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essensgeldkasse.Database
{

    public class ClientsDatabase
    {
        SQLiteAsyncConnection Database;
        List<Product> products = new List<Product>();
        Clients clients = new Clients();
        public ClientsDatabase()
        {

        }

       public async Task Init()
        {

            if (Database is not null)
            { return; }
            else
            {
                Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);


                var result = await Database.CreateTableAsync<Client>();


                var res = await Database.CreateTableAsync<Product>();





            }
                        

        }
        
        public async Task<List<Client>> GetClientsAsync(string orderBy, string asc_dsc)
        {
            await Init();
            if (orderBy.Equals("LastName") && asc_dsc.Equals("asc"))
            {
                return await Database.Table<Client>().OrderBy(i => i.LastName).ToListAsync();
            }
            if (orderBy.Equals("LastName") && asc_dsc.Equals("dsc"))
            {
                return await Database.Table<Client>().OrderByDescending(i => i.LastName).ToListAsync();
            }
            if (orderBy.Equals("FirstName") && asc_dsc.Equals("asc"))
            {
                return await Database.Table<Client>().OrderBy(i => i.FirstName).ToListAsync();
            }
            if (orderBy.Equals("FirstName") && asc_dsc.Equals("dsc"))
            {
                return await Database.Table<Client>().OrderByDescending(i => i.FirstName).ToListAsync();
            }
            if (orderBy.Equals("Code") && asc_dsc.Equals("asc"))
            {
                return await Database.Table<Client>().OrderBy(i => i.Code).ToListAsync();
            }
            if (orderBy.Equals("Code") && asc_dsc.Equals("dsc"))
            {
                return await Database.Table<Client>().OrderByDescending(i => i.Code).ToListAsync();
            }
            return await Database.Table<Client>().ToListAsync();
        }


        public async Task<Client> GetClientAsync(int id)
        {
            await Init();
            return await Database.Table<Client>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveClientAsync(Client item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteClientAsync(Client item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
        public async Task<List<Product>> GetProductsAsync()
        {
            await Init();
            return await Database.Table<Product>().ToListAsync();
        }
        public async Task<Product> GetProductAsync(int id)
        {
            await Init();
            return await Database.Table<Product>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveProductAsync(Product item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteProductAsync(Product item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
        public async Task<List<SQLiteConnection.ColumnInfo>> GetTableInfos(string TableName)
        {
            var infos = await Database.GetTableInfoAsync(TableName);
            if (!infos.Any())
            {
                return new List<SQLiteConnection.ColumnInfo>();
            }
            else return infos;
        }
    }
}
