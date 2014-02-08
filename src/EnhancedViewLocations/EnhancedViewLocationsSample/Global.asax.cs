using System;
using System.Web.Mvc;
using System.Web.Routing;
using EnhancedViewLocations;

namespace EnhancedViewLocationsSample
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters( GlobalFilters.Filters );
			RegisterRoutes( RouteTable.Routes );

			// Register the extra locations for views.
			RegisterCustomViewLocations();
		}

		private static void RegisterCustomViewLocations()
		{
			// We can optionally enable the standard Razor view CS (or VB) locations 
			// so we don't always search our extra / custom areas first when 
			// searching for a view.
			EnhancedViewLocator.EnableStandardRazorCSLocations();


			// Next, add two extra locations to store / arrange our views.

			// 1. in the templates folder under the standard shared view folder.
			EnhancedViewLocator.AddFolder( "views/shared/templates" );

			// 2. in the navigation folder under the standard shared view folder.
			EnhancedViewLocator.AddFolder( "views/shared/navigation" );

			// Finally, have the locator install a custom view engine to manage the lookups.
			EnhancedViewLocator.Install( ControllerBuilder.Current );
		}

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add( new HandleErrorAttribute() );
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute( "{resource}.axd/{*pathInfo}" );

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}
	}
}