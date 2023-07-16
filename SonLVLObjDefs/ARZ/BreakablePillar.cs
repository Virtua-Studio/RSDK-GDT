using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class BreakablePillar : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[2];
			
			BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(59, 42, 56, 32), -28, -24); // different from the RetroED preview, this is what the Pillar looks like after fully extending
			
			frames[1] = new Sprite(sheet.GetSection(223, 137, 32, 24), -16, 8); // no grass frame
			sprites[0] = new Sprite(frames);
			
			frames[1] = new Sprite(sheet.GetSection(71, 130, 32, 16), -16, 8); // grass frame
			sprites[1] = new Sprite(frames);
			
			properties[0] = new PropertySpec("Hide Grass", typeof(bool), "Extended",
				"If the bottom of this Pillar should have grass or not. Purely cosmetic, does not affect actual in-game behaviour.", null, 
				(obj) => (obj.PropertyValue > 0),
				(obj, value) => obj.PropertyValue = (byte)(((bool)value) ? 1 : 0));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype > 0) ? "Hide Grass" : "Show Grass";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[Math.Min(subtype, (byte)1)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[Math.Min(obj.PropertyValue, (byte)1)];
		}
	}
}
