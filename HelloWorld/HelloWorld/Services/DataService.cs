using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace HelloWorld.Services
{
    public class DataService
    {
        private static DataService _instance;

        private readonly MobileServiceClient _mobileServiceClient;
        private readonly IMobileServiceSyncTable<InventoryItem> _inventoryTable;

        private DataService()
        {
            CurrentPlatform.Init();
            SQLitePCL.CurrentPlatform.Init();

            var applicartionUrl = Configuration.MobileService.Address;
            _mobileServiceClient = new MobileServiceClient(applicartionUrl, Configuration.MobileService.Key);
            _inventoryTable = _mobileServiceClient.GetSyncTable<InventoryItem>();
        }

        public List<InventoryItem> Items { get; private set; }

        public async Task InitializeStoreAsync()
        {
            var store = new MobileServiceSQLiteStore(Configuration.Database.LocalDbPath);

            store.DefineTable<InventoryItem>();

            // Uses the default conflict handler, which fails on conflict
            // To use a different conflict handler, pass a parameter to InitializeAsync. For more details, see http://go.microsoft.com/fwlink/?LinkId=521416
            await _mobileServiceClient.SyncContext.InitializeAsync(store);
        }

        public static DataService DefaultService
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new DataService();
                    _instance.InitializeStoreAsync().Wait();
                }
                return _instance;
            }
        }


        public async Task<List<InventoryItem>> RefreshDataAsync()
        {
            try
            {
                // update the local store
                // all operations on todoTable use the local database, call SyncAsync to send changes
                await _inventoryTable.PullAsync("InventoryItems", _inventoryTable.CreateQuery()); // query ID is used for incremental sync


                // This code refreshes the entries in the list view by querying the local TodoItems table.
                // The query excludes completed TodoItems
                Items = await _inventoryTable.ToListAsync();

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(@"ERROR {0}", e.Message);
                return null;
            }

            return Items;
        }
    }
}