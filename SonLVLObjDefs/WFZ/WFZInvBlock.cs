using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class WFZInvBlock : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[3];

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 176, 16, 14), -8, -7);
			
			properties[0] = new PropertySpec("Width", typeof(int), "Extended",
				"How wide the Invisible Block will be.", null,
				(obj) => (obj.PropertyValue >> 4) + 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 15) | (byte)(Math.Max((((((int)value) & 15) << 4) - 1), 0))));
			
			properties[1] = new PropertySpec("Height", typeof(int), "Extended",
				"How tall the Invisible Block will be.", null,
				(obj) => (obj.PropertyValue & 15) + 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 240) | (byte)(Math.Max(((((int)value) & 15) - 1), 0))));
			
			properties[2] = new PropertySpec("Time Attack Only", typeof(bool), "Extended",
				"If this Block should only be in Time Attack.", null,
				(obj) => ((V4ObjectEntry)obj).Value0 == 1,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((bool)value ? 1 : 0));
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
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int width = (obj.PropertyValue >> 4) + 1;
			int height = (obj.PropertyValue & 15) + 1;
			
			int sx = (obj.PropertyValue & 240) >> 1;
			int sy = (obj.PropertyValue & 15) << 3;
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					sprs.Add(new Sprite(sprite, -sx + (j * 16), -sy + (i * 16)));
				}
			}
			return new Sprite(sprs.ToArray());
		}
	}
}