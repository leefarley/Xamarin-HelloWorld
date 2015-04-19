
using System;

using Foundation;
using UIKit;

namespace HelloWorld
{
	public partial class MyViewController : UIViewController
	{
		private int _clickCount;
		public MyViewController () : base ("MyViewController", null)
		{
			_clickCount = 0;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			addClickButton.TouchUpInside += addClickButton_TouchUpInside;
			removeClickButton.TouchUpInside += removeClickButton_TouchUpInside;
		}

		private void addClickButton_TouchUpInside(object sender, EventArgs args)
		{
			_clickCount += 1;
			clickCount.Text = _clickCount.ToString();
		}

		private void removeClickButton_TouchUpInside(object sender, EventArgs args)
		{
			_clickCount -= 1;
			clickCount.Text = _clickCount.ToString();
		}
	}
}

