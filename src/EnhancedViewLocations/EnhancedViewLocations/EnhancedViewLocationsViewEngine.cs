using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EnhancedViewLocations
{
	internal class EnhancedViewLocationsViewEngine : RazorViewEngine
	{

		public EnhancedViewLocationsViewEngine()
			: this( null )
		{
		}

		public EnhancedViewLocationsViewEngine(IViewPageActivator viewPageActivator)
			: base( viewPageActivator )
		{
		}

		internal void SetFullViewFormats(List<string> viewFormats)
		{
			base.ViewLocationFormats = viewFormats.ToArray();
		}

		internal void SetPartialViewFormats(List<string> viewFormats)
		{
			base.PartialViewLocationFormats = viewFormats.ToArray();
		}

		internal void SetFileExtensions(List<string> fileExtensions)
		{
			base.FileExtensions = fileExtensions.ToArray();
		}
	}
}
