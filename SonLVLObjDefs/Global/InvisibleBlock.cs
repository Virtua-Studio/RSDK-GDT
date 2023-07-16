using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Global
{
	class InvisibleBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[3];
		private readonly Sprite[] sprites = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			sprites[0] = new Sprite(sheet.GetSection(1, 176, 16, 14), -8, -7);
			sprites[1] = new Sprite(sheet.GetSection(17, 176, 16, 14), -8, -7);
			sprites[2] = new Sprite(sheet.GetSection(1, 190, 16, 14), -8, -7);
			
			properties[0] = new PropertySpec("Width", typeof(int), "Extended",
				"How wide the Invisible Block will be.", null,
				(obj) => (obj.PropertyValue >> 4) + 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0xf0) | (byte)(Math.Max((((((int)value) & 0x0f) << 4) - 1), 0))));
			
			properties[1] = new PropertySpec("Height", typeof(int), "Extended",
				"How tall the Invisible Block will be.", null,
				(obj) => (obj.PropertyValue & 0x0f) + 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x0f) | (byte)(Math.Max(((((int)value) & 0x0f) - 1), 0))));
			
			properties[2] = new PropertySpec("Mode", typeof(int), "Extended",
				"Which behaviour the Invisible Block will assume.", null, new Dictionary<string, int>
				{
					{ "Solid", 0 },
					{ "Eject Left", 1 },
					{ "Eject Right", 2 }
				},
				(obj) => ((V4ObjectEntry)obj).State,
				(obj, value) => ((V4ObjectEntry)obj).State = ((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x11; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int width = (obj.PropertyValue >> 4) + 1;
			int height = (obj.PropertyValue & 0x0f) + 1;
			
			int sx = (obj.PropertyValue & 0xf0) >> 1;
			int sy = (obj.PropertyValue & 0x0f) << 3;
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					sprs.Add(new Sprite(sprites[Math.Min(((V4ObjectEntry)obj).State, 2)], -sx + (j * 16), -sy + (i * 16)));
				}
			}
			
			return new Sprite(sprs.ToArray());
		}
	}
}