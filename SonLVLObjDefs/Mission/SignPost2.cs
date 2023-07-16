using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Mission
{
	class SignPost2 : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties = new PropertySpec[3];

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items2.gif");
			Sprite post = new Sprite(sheet.GetSection(34, 132, 48, 16), -24, 16);
			sprites[0] = new Sprite(new Sprite(sheet.GetSection(34, 100, 48, 32), -24, -16), post);
			sprites[1] = new Sprite(new Sprite(sheet.GetSection(34, 1, 48, 32), -24, -16), post);
			sprites[2] = new Sprite(new Sprite(sheet.GetSection(34, 34, 48, 32), -24, -16), post);
			
			properties[0] = new PropertySpec("Mode", typeof(int), "Extended",
				"Used to differentiate multiplayer Signposts from regular Signposts.", null, new Dictionary<string, int>
				{
					{ "Normal", 0 },
					{ "VS Only (Y Bounds)", 1 },
					{ "VS Only (No Y Bounds)", 2 }
				},
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			properties[1] = new PropertySpec("Center Offset", typeof(int), "Extended",
				"How offset the Signpost's camera will be. X-wise. Measured in pixels, to every 16th pixel.", null,
				(obj) => ((V4ObjectEntry)obj).Value0 << 4,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((int)value) >> 4);
			
			properties[2] = new PropertySpec("Exit Right", typeof(bool), "Extended",
				"If the Signpost should make the player move right afterwards.", null,
				(obj) => (((V4ObjectEntry)obj).Value1 == 1),
				(obj, value) => ((V4ObjectEntry)obj).Value1 = ((bool)value == false ? 0 : 1));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Normal";
				default:
				case 1:  return "VS Only (Y Bounds)";
				case 2: return "VS Only (No Y Bounds)";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[((obj.PropertyValue == 2) || (obj.PropertyValue == 0)) ? obj.PropertyValue : 1];
		}
	}
}