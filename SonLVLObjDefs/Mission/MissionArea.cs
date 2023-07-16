using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.Mission
{
	class MissionAreaTR : MissionAreaBL
	{
		public override bool GetSpriteFlip() { return true; }
	}
	
	class MissionAreaBL : ObjectDefinition
	{
		private Sprite sprite;
		
		public virtual bool GetSpriteFlip() { return false; }
		
		public override void Init(ObjectData data)
		{
			// mimick the CD/3K bounds corner sprite
			BitmapBits bitmap = new BitmapBits(33, 33);
			bitmap.FillRectangle(1, 0, 0, 32, 32); // black, main bg
			bitmap.FillRectangle(6, 1, 1, 30, 30); // white, main bg
			bitmap.FillRectangle(1, 3, 3, 3, 26); // black, vertical line
			bitmap.FillRectangle(1, 3, 26, 26, 3); // black, horizontal line
			sprite = new Sprite(bitmap, -16, -16);
			sprite.Flip(GetSpriteFlip(), GetSpriteFlip());
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
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
	}
}