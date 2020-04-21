using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ProjectPosition2
{
    // Token: 0x02000002 RID: 2
    [Transaction(TransactionMode.Manual)]
	[Regeneration(0)]
	internal class Command
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
		public void ShowActiveProjectLocationUsage(Document document)
		{
			ProjectLocation activeProjectLocation = document.ActiveProjectLocation;
			XYZ xyz = new XYZ(0.0, 0.0, 0.0);
			ProjectPosition projectPosition = activeProjectLocation.GetProjectPosition(xyz);
			bool flag = projectPosition == null;
			if (flag)
			{
				throw new Exception("No project position in origin point.");
			}
			string text = "Current project location information:\n";
			text += "\n\tOrigin point position:";
			text = text + "\n\t\tAngle: " + projectPosition.Angle;
			text = text + "\n\t\tEast to West offset: " + projectPosition.EastWest;
			text = text + "\n\t\tElevation: " + projectPosition.Elevation;
			text = text + "\n\t\tNorth to South offset: " + projectPosition.NorthSouth;
			SiteLocation siteLocation = activeProjectLocation.GetSiteLocation();
			string text2 = '°'.ToString();
			text += "\n\tSite location:";
			text = string.Concat(new object[]
			{
				text,
				"\n\t\tLatitude: ",
				siteLocation.Latitude / 0.017453292519943295,
				text2
			});
			text = string.Concat(new object[]
			{
				text,
				"\n\t\tLongitude: ",
				siteLocation.Longitude / 0.017453292519943295,
				text2
			});
			text = text + "\n\t\tTimeZone: " + siteLocation.TimeZone;
			TaskDialog.Show("Revit", text);
		}
	}
}
