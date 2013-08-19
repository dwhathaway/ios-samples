using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace GoogleMaps
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UIViewController controller; 
		UINavigationController navController;
		UISwitch emulateNoGmaps; 

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			// If you have defined a root view controller, set it here:
			controller = new UIViewController();
			controller.View.BackgroundColor = UIColor.White;
			controller.Title = "My Controller";

			UIButton mapButton = new UIButton (UIButtonType.RoundedRect);
			mapButton.SetTitle ("Open Maps", UIControlState.Normal);
			mapButton.Frame = new RectangleF (window.Frame.Width / 2 - 50, 100, 100, 44);

			mapButton.TouchUpInside += (object sender, EventArgs e) => {
				//If emulateNoGmaps is on, then lauch Apple maps instead
				string mapsScheme = emulateNoGmaps.On ? string.Empty : "comgooglemaps://?center=40.765819,-73.975866&zoom=14&views=traffic";
				NSUrl gmaps = new NSUrl(mapsScheme);
				if (!UIApplication.SharedApplication.OpenUrl(gmaps))
				{
					var av = new UIAlertView("Google Maps"
					                         , "It appears as though Google Maps isn't installed.  Would you like to install it?"
					                         , null
					                         , "Yes Please"
					                         , "No Thanks");
					av.Show();

					av.Clicked += (sender1, buttonArgs) => {
						if(buttonArgs.ButtonIndex == 0) {
							NSUrl badMaps = new NSUrl("https://itunes.apple.com/us/app/google-maps/id585027354?mt=8");
							UIApplication.SharedApplication.OpenUrl(badMaps);
						}
						else {
							NSUrl badMaps = new NSUrl("http://maps.apple.com/?ll=40.765819,-73.975866");
							//NSUrl badMaps = new NSUrl("https://itunes.apple.com/us/app/google-maps/id585027354?mt=8");
							UIApplication.SharedApplication.OpenUrl(badMaps);

						}
					};
				}
			};

			emulateNoGmaps = new UISwitch(new RectangleF (window.Frame.Width / 2 - 50, 50, 100, 44));
			emulateNoGmaps.On = false;

			controller.Add (mapButton);
			controller.Add (emulateNoGmaps);

			navController = new UINavigationController(controller);
			window.RootViewController = navController;
			
			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

