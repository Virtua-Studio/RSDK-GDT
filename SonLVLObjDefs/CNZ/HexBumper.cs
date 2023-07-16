using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class HexBumper : ObjectDefinition
	{
		private Sprite img;
		private Sprite debug;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
			{
				img = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(99, 99, 48, 32), -24, -16);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(581, 343, 48, 32), -24, -16);
			}

			BitmapBits bitmap = new BitmapBits(193, 2);
			bitmap.DrawLine(6, 0, 0, 192, 0); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -96, 0);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Moving", typeof(int), "Extended",
				"If the Bumper will be Moving or not.", null, new Dictionary<string, int>
				{
					{ "Static", 0 },
					{ "Moving", 1 }
				},
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = ((byte)((int)value)));
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
			return (subtype == 1) ? "Moving Bumper" : "Static Bumper";
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue == 1)
			{
				return debug;
			}
			return new Sprite();
		}
	}
}