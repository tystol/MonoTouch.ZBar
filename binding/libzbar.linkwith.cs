using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libzbar.a", LinkTarget.ArmV6 | LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true, Frameworks = "CoreGraphics AVFoundation CoreMedia CoreVideo QuartzCore", LinkerFlags = "-liconv")]
