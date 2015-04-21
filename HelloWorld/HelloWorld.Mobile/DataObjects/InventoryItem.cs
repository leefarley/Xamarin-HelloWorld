using Microsoft.WindowsAzure.Mobile.Service;

namespace HelloWorld.Mobile.DataObjects
{
    public class InventoryItem : EntityData
    {
        public string Name { get; set; }

        public int Stock { get; set; }
    }
}