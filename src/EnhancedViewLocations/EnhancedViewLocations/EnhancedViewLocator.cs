using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EnhancedViewLocations
{
	public class EnhancedViewLocator
	{
		private static List<string> ViewFormats { get; set; }
		private static List<string> FileExtensions { get; set; }

		static EnhancedViewLocator()
		{
			ViewFormats = new List<string>();
			FileExtensions = new List<string>();
		}

		public static void Install(ControllerBuilder builder)
		{
			if ( ViewFormats.Count == 0 )
			{
				throw new InvalidOperationException( 
					"You must enable at least one set of view locations or add your own." 
					);
			}

			if ( FileExtensions.Count == 0 )
			{
				FileExtensions.Add( "cshtml" );
				FileExtensions.Add( "vbhtml" );
			}

			var engine = new EnhancedViewLocationsViewEngine();
			engine.SetFullViewFormats( ViewFormats );
			engine.SetPartialViewFormats( ViewFormats );
			engine.SetFileExtensions( FileExtensions );

			ViewEngines.Engines.Insert( 0, engine );
		}

		public static void AddFolder(string folder, string extensionWithoutDot = "cshtml")
		{
			ViewFormats.Add( "~/" + folder.Trim( '/' ) + "/{0}." + extensionWithoutDot );
			ViewFormats.Add( "~/" + folder.Trim( '/' ) + "/{1}/{0}." + extensionWithoutDot );
		}

		public static void AddLocation(string locationFormat)
		{
			AddLocation( locationFormat, false );
		}

		public static void AddLocation(string locationFormat, bool insertFirst)
		{
			if ( !locationFormat.ToLower().Contains( "{0}" ) )
			{
				throw new FormatException( "You must specify a template format with '{0}', e.g. ~/Views/{1}/{0}.cshtml" );
			}

			if ( insertFirst )
			{
				ViewFormats.Insert( 0, locationFormat );
			}
			else
			{
				ViewFormats.Add( locationFormat );
			}
		}

		public static void EnableStandardRazorCSLocations()
		{
			FileExtensions.Add( "cshtml" );
			ViewFormats.Add( "~/Views/{1}/{0}.cshtml" );
			ViewFormats.Add( "~/Views/{1}/{0}.cshtml" );
			ViewFormats.Add( "~/Views/Shared/{0}.cshtml" );
			ViewFormats.Add( "~/Views/Shared/{0}.cshtml" );
		}

		public static void EnableStandardRazorVBLocations()
		{
			FileExtensions.Add( "vbhtml" );
			ViewFormats.Add( "~/Views/{1}/{0}.vbhtml" );
			ViewFormats.Add( "~/Views/{1}/{0}.vbhtml" );
			ViewFormats.Add( "~/Views/Shared/{0}.vbhtml" );
			ViewFormats.Add( "~/Views/Shared/{0}.vbhtml" );
		}
	}
}
