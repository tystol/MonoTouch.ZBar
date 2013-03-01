using System;
using System.Reflection;
using System.Runtime.InteropServices;

using MonoTouch;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

[assembly: AssemblyTitle("ZBar.MonoTouch")]
[assembly: AssemblyVersion( ZBar.ZBarSDK.iOSZBarVerison + ".0" )]

namespace ZBar
{
	public static partial class ZBarSDK
	{
        internal const string iOSZBarVerison = "1.2.0";
		
        // http://stackoverflow.com/questions/10055053/exposing-an-obj-c-const-nsstring-via-a-monotouch-binding
//		public static NSString BarcodeResultsKey
//		{
//			get 
//			{
//				return new NSString("ZBarReaderControllerResults");
//				
//				// var libraryHandle = Dlfcn.dlopen("libzbar.a", 0 );
//				// return Dlfcn.GetStringConstant(libraryHandle, "ZBarReaderControllerResults");
//			}
//		}
			
		/** retrieve runtime library version information.
		 * @param major set to the running major version (unless NULL)
		 * @param minor set to the running minor version (unless NULL)
		 * @returns 0
		 */
		// extern int zbar_version(unsigned *major, unsigned *minor);
		
		[DllImport ("__Internal")]
		static extern int zbar_version (IntPtr major, IntPtr minor);
		
		public static string GetCoreZBarVersion()
		{
			uint major = 0;
			uint minor = 0;
			
			unsafe
			{
				IntPtr majorPointer = (IntPtr) (&major);
				IntPtr minorPointer = (IntPtr) (&minor);
				
				zbar_version(majorPointer, minorPointer);
			}
			
			return String.Format("{0}.{1}", major, minor);
		}

        public static string GetiOSZBarVersion()
        {
            return iOSZBarVerison;
        }
	}
}

