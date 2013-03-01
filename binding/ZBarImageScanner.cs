using System;

namespace ZBar
{
	public partial class ZBarImageScanner
	{
		public void EnableSymbol(ZBarSymbolType symbolType)
		{
			SetSymbolOption(symbolType,ZBarConfig.Enabled,1);
		}
		
		public void DisableSymbol(ZBarSymbolType symbolType)
		{
			SetSymbolOption(symbolType,ZBarConfig.Enabled,0);
		}
	}
}

