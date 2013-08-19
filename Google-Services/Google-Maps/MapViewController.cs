using System;
using MonoTouch.UIKit;

namespace GoogleMaps
{
	public class MapViewController : UIViewController
	{
		public MapViewController ()
		{
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.Gray;
		}
	}
}

