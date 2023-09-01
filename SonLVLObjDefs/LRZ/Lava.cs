using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Global
{
	class InvisibleBlock : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[3];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			sprites[0] = new Sprite(sheet.GetSection(1, 176, 16, 14), -8, -7);
			sprites[1] = new Sprite(sheet.GetSection(17, 176, 16, 14), -8, -7);
			sprites[2] = new Sprite(sheet.GetSection(1, 190, 16, 14), -8, -7);
			
			properties = new PropertySpec[3];
			properties[0] = new PropertySpec("Width", typeof(int), "Extended",
				"How wide the Invisible Block will be.", null,
				(obj) => obj.PropertyValue >> 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 15) | (byte)((((int)value) & 15) << 4)));
			
			properties[1] = new PropertySpec("Height", typeof(int), "Extended",
				"How tall the Invisible Block will be.", null,
				(obj) => obj.PropertyValue & 15,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 240) | (byte)(((int)value) & 15)));
			
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
		
		public override byte DefaultSubtype
		{
			get { return 17; }
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
			int width = obj.PropertyValue >> 4;
			int height = obj.PropertyValue & 15;
			width += 1; height += 1;
			
			int sx = (obj.PropertyValue & 240) << 15;
			int sy = (obj.PropertyValue & 15) << 19;
			sx >>= 16; sy >>= 16;
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Sprite tmp = new Sprite(sprites[Math.Min(((V4ObjectEntry)obj).State, 2)]);
					tmp.Offset(-sx + (j * 16), -sy + (i * 16));
					sprs.Add(tmp);
				}
			}
			return new Sprite(sprs.ToArray());
		}
	}
}