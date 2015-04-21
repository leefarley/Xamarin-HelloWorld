using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using HelloWorld.Mobile.DataObjects;
using HelloWorld.Mobile.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace HelloWorld.Mobile
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            var inventoryItems = new List<InventoryItem>
            {
                new InventoryItem { Id = Guid.NewGuid().ToString(), Name = "First item", Stock = 5 },
                new InventoryItem { Id = Guid.NewGuid().ToString(), Name = "Second item", Stock = 2 },
            };

            foreach (InventoryItem inventoryItem in inventoryItems)
            {
                context.Set<InventoryItem>().Add(inventoryItem);
            }

            base.Seed(context);
        }
    }
}

