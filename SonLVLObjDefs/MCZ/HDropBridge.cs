using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MCZ
{
	class HDropBridge : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite img = new Sprite(LevelData.GetSpriteSheet("MCZ/Objects.gif").GetSection(135, 131, 16, 16), -8, -8);
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 4; i++)
			{
				sprs.Add(new Sprite(img, (i * 16) - 56, 0));
				sprs.Add(new Sprite(img, 56 - (i * 16), 0));
			}
			
			sprite = new Sprite(sprs.ToArray());
			
			BitmapBits bitmap = new BitmapBits(129, 65);
			bitmap.DrawRectangle(6, 0, 0, 16, 64); // LevelData.ColorWhite
			bitmap.DrawRectangle(6, 112, 0, 16, 64); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -64, -8);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "";
		}
		
		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}