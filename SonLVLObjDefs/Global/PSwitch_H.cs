using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Global
{
	class PSwitch_H : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[21];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			
			int index = 0;
			for (int i = 0; i < 8; i++)
			{
				sprites[index] = new Sprite(sheet.GetSection(42 + (i * 17), 130, 16, 16), -8, -8);
				index++;
			}
			
			for (int i = 0; i < 8; i++)
			{
				sprites[index] = new Sprite(sheet.GetSection(42 + (i * 17), 147, 16, 16), -8, -8);
				index++;
			}
			
			sprites[16] = new Sprite(sheet.GetSection(42, 164, 16, 16), -8, -8);
			sprites[17] = new Sprite(sheet.GetSection(59, 164, 16, 16), -8, -8);
			sprites[18] = new Sprite(sheet.GetSection(76, 164, 16, 16), -8, -8);
			sprites[19] = new Sprite(sheet.GetSection(93, 164, 16, 16), -8, -8);
			sprites[20] = new Sprite(sheet.GetSection(93, 113, 16, 16), -8, -8);
			
			properties = new PropertySpec[7];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How wide the Plane Switch will be.", null, new Dictionary<string, int>
				{
					{ "4 Nodes", 0 },
					{ "8 Nodes", 1 },
					{ "16 Nodes", 2 },
					{ "32 Nodes", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 252) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Top Collision Plane", typeof(int), "Extended",
				"Which plane is above.", null, new Dictionary<string, int>
				{
					{ "Plane A", 0 },
					{ "Plane B", 4 }
				},
				(obj) => obj.PropertyValue & 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 251) | (byte)((int)value)));
			
			properties[2] = new PropertySpec("Bottom Collision Plane", typeof(int), "Extended",
				"Which plane is below.", null, new Dictionary<string, int>
				{
					{ "Plane A", 0 },
					{ "Plane B", 8 }
				},
				(obj) => obj.PropertyValue & 8,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 247) | (byte)((int)value)));
			
			properties[3] = new PropertySpec("Top Draw Order", typeof(int), "Extended",
				"Which draw layer is above.", null, new Dictionary<string, int>
				{
					{ "Low Layer", 0 },
					{ "High Layer", 16 }
				},
				(obj) => obj.PropertyValue & 16,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 239) | (byte)((int)value)));
			
			properties[4] = new PropertySpec("Bottom Draw Order", typeof(int), "Extended",
				"Which draw layer is below.", null, new Dictionary<string, int>
				{
					{ "Low Layer", 0 },
					{ "High Layer", 32 }
				},
				(obj) => obj.PropertyValue & 32,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 223) | (byte)((int)value)));
			
			properties[5] = new PropertySpec("Only Draw Order", typeof(int), "Extended",
				"If only Draw Order should be affected.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 64 }
				},
				(obj) => obj.PropertyValue & 64,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 191) | (byte)((int)value)));
			
			properties[6] = new PropertySpec("Grounded", typeof(int), "Extended",
				"If only grounded players should be affected.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 128 }
				},
				(obj) => obj.PropertyValue & 128,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 127) | (byte)((int)value)));
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
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
			get { return sprites[16]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[16];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int count = Math.Max((1 << ((obj.PropertyValue & 3) + 2)), 1);
			int frame = (obj.PropertyValue >> 2) & 15;
			int temp = obj.PropertyValue & 64;
			if (temp != 0)
				frame = (frame >> 2) + 16;
			int sx = -(((count) * 16) / 2) + 8;
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < count; i++)
			{
				if (obj.PropertyValue > 127)
				{
					Sprite back = new Sprite(sprites[16]);
					back.Offset(sx + (i * 16), 0);
					sprs.Add(back);
				}
				
				Sprite tmp = new Sprite(sprites[frame]);
				tmp.Offset(sx + (i * 16), 0);
				sprs.Add(tmp);
			}
			return new Sprite(sprs.ToArray());
		}
	}
}