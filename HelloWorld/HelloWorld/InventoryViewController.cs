using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using HelloWorld.Models;
using HelloWorld.Services;
using UIKit;

namespace HelloWorld
{
    public partial class InventoryViewController : UITableViewController
    {
        private DataService _dataService;

        public InventoryViewController(IntPtr handle) : base(handle)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();

            _dataService = DataService.DefaultService;


            await RefreshAsync();


            // Perform any additional setup after loading the view, typically from a nib.
        }

        private async Task RefreshAsync()
        {
            await _dataService.RefreshDataAsync();
            TableView.Source = new InventoryTableSource(_dataService.Items);
            TableView.ReloadData();
        }

    }

    public class InventoryTableSource : UITableViewSource
    {
        private readonly IList<InventoryItem> _inventory;
        private const string CellIdentifier = "InventoryTableCell";

        public InventoryTableSource(IList<InventoryItem> inventory)
        {
            _inventory = inventory ?? new List<InventoryItem>();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Value1, CellIdentifier);

            InventoryItem item = _inventory[indexPath.Row];
            cell.TextLabel.Text = item.Name;
            cell.DetailTextLabel.Text = string.Format("Stock : {0}", item.Stock);
            if (UInt32.Parse(item.Stock) < 3)
            {
                cell.DetailTextLabel.TextColor = UIColor.Red;
            }
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _inventory.Count;
        }
    }
}