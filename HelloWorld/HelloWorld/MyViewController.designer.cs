// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace HelloWorld
{
	[Register ("MyViewController")]
	partial class MyViewController
	{
		[Outlet]
		UIKit.UIButton addClickButton { get; set; }

		[Outlet]
		UIKit.UILabel clickCount { get; set; }

		[Outlet]
		UIKit.UIButton removeClickButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (addClickButton != null) {
				addClickButton.Dispose ();
				addClickButton = null;
			}

			if (removeClickButton != null) {
				removeClickButton.Dispose ();
				removeClickButton = null;
			}

			if (clickCount != null) {
				clickCount.Dispose ();
				clickCount = null;
			}
		}
	}
}
