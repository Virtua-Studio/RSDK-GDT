using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class TargetBumper : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("CNZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(1, 166, 32, 12), -16, -6);
			sprites[1] = new Sprite(sheet.GetSection(1, 229, 24, 24), -12, -12);
			sprites[2] = new Sprite(sheet.GetSection(60, 140, 12, 32), -6, -16);
			
			properties[0] = new PropertySpec("Orientation", typeof(int), "Extended",
				"How the Bumper is facing.", null, new Dictionary<string, int>
				{
					{ "Vertical", 0 },
					{ "Diagonal (Right)", 1 },
					{ "Diagonal (Left)", 2 },
					{ "Horizontal (Right)", 3 },
					{ "Horizontal (Left)", 4 }
				},
				(obj) => obj.PropertyValue & 0x3f,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x3f) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Combo Type", typeof(int), "Extended",
				"The conditions needed for this Bumper's combo reward.", null, new Dictionary<string, int>
				{
					{ "Left (obj[+1] & obj[+2] are required)", 0 },
					{ "Middle (obj[-1] & obj[+1] are required)", 64 },
					{ "Right (obj[-1] & obj[-2] are required)", 128 },
					{ "Standalone", 192 }
				},
				(obj) => obj.PropertyValue & 0xc0,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0xc0) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
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
			// this whole thing's a little bit weird, i hope you don't mind
			
			int frame = 0;
			bool flip = false;
			switch (obj.PropertyValue & 0x3f)
			{
				case 2:
					flip = true;
					goto case 1;
				case 1:
					frame = 1;
					break;
				case 4:
					flip = true;
					goto case 3;
				case 3:
					frame = 2;
					break;
			}
			return new Sprite(sprites[frame], flip, false);
		}
		
		// For the combo variable, maybe we could draw lines to the connecting objects but i dunno if that'd be too cluttered...
	}
}