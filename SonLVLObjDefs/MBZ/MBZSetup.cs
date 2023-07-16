using SonicRetro.SonLVL.API;
using System;
using System.Drawing;

namespace S2ObjectDefinitions.MBZ
{
	public class MBZSetup : DefaultObjectDefinition
	{
		public override void Init(ObjectData data)
		{
			// MBZ's tile palette uses an orange for boss blacks while in-game uses, well, black, so
			// we do a little orange->black correction here, only affects object panel and not actual palette
			LevelData.BmpPal.Entries[192] = Color.Black;
			
			base.Init(data);
		}
	}
}