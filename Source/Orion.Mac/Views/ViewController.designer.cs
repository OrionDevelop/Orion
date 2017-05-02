// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Orion.Mac
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField yuiSong { get; set; }

		[Action ("ClickedButton:")]
		partial void ClickedButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (yuiSong != null) {
				yuiSong.Dispose ();
				yuiSong = null;
			}
		}
	}
}
