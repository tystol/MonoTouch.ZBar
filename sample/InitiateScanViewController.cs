using System;
using MonoTouch.UIKit;
using ZBar;
using System.Drawing;
using System.Text;
using MonoTouch.Foundation;

namespace ZBar.Sample
{
    public class InitiateScanViewController : UIViewController
    {
        private UILabel resultsLabel;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;

            var width = View.Bounds.Width;
            var scanButton = new UIButton(UIButtonType.RoundedRect)
            {
                Frame = new RectangleF(50,50,width-100,44),
                AutoresizingMask = UIViewAutoresizing.FlexibleBottomMargin | UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin,
            };
            scanButton.SetTitle("Scan", UIControlState.Normal);
            scanButton.TouchUpInside += (o,e) =>
            {
                var barcodeReader = new ZBarReaderViewController();
                barcodeReader.ReaderDelegate = new BarcodeReaderCallback(this);
                barcodeReader.SupportedOrientations = ZBarOrientation.All;
                barcodeReader.ShowsZBarControls = true;

                // Access the inner ZBarImageScanner to tweak scanner settings.
                var scanner = barcodeReader.Scanner;
                // Disable all symbol types.
                scanner.SetSymbolOption(ZBarSymbolType.All, ZBarConfig.Enabled, 0);
                // Then for this sample enable just QR.
                scanner.EnableSymbol(ZBarSymbolType.QRCode);

                PresentViewController(barcodeReader, true, null);
            };
            View.AddSubview(scanButton);

            resultsLabel = new UILabel(new RectangleF(10,110,width-20,200))
            {
                AutoresizingMask = UIViewAutoresizing.FlexibleDimensions,
                Lines = 0,
                Font = UIFont.SystemFontOfSize(14),
            };
            View.AddSubview(resultsLabel);
        }

        private void ZBarSymbolsDetected(ZBarSymbolSet symbols, UIImagePickerController barcodeReader)
        {
            var resultString = new StringBuilder();
            foreach ( var symbol in symbols )
            {
                resultString.AppendFormat("{0}: {1}", symbol.SymbolType, symbol.Data);
                resultString.AppendLine();
            }

            resultsLabel.Text = resultString.ToString();
            barcodeReader.DismissViewController(true, null);
        }

        private class BarcodeReaderCallback : ZBarReaderDelegate
        {
            private readonly InitiateScanViewController parentController;
            
            public BarcodeReaderCallback(InitiateScanViewController parentController)
            {
                this.parentController = parentController;
            }
            
            public override void FinishedPickingMedia (UIImagePickerController picker, NSDictionary info)
            {
                var results = info[ZBarSDK.BarcodeResultsKey];
                
                var symbolSet = results as ZBarSymbolSet;
                if ( symbolSet != null )
                    parentController.ZBarSymbolsDetected(symbolSet, picker);
                else
                    Console.WriteLine("ZBar result not a ZBarSymbolSet ({0}).",results.GetType());
            }
            
            public override void Canceled(UIImagePickerController picker)
            {
                Console.WriteLine("ZBarReaderViewController Cancelled!");
                picker.DismissViewController(true, null);
            }
        }
    }
}

