using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class RotatePlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[3];
		private readonly Sprite[] sprites = new Sprite[3];
		private Sprite debug;

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(160, 235, 30, 20), -15, -12);
			sprites[1] = new Sprite(sheet.GetSection(174, 218, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(126, 191, 64, 16), -32, -8);

			int radius = 64;
			var overlay = new BitmapBits(radius * 2 + 1, radius * 2 + 1);
			overlay.DrawCircle(6, radius, radius, radius); // LevelData.ColorWhite
			debug = new Sprite(overlay, -radius, -radius - 4);
			
			properties[0] = new PropertySpec("Offset", typeof(int), "Extended",
				"The offset/starting angle of the platform.", null,
				(obj) => (obj.PropertyValue & 0x0f),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x0f) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Speed", typeof(int), "Extended",
				"The speed of the platform.", null,
				(obj) => (obj.PropertyValue & 0x70) >> 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x70) | (byte)((int)value << 4)));
			
			properties[2] = new PropertySpec("Direction", typeof(int), "Extended",
				"The direction in which the Platform moves.", null, new Dictionary<string, int>
				{
					{ "Clockwise", 0 },
					{ "Counter-clockwise", 1 }
				},
				(obj) => (obj.PropertyValue >> 7),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | (byte)((int)value << 7)));
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
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2];
		}

		public override Rectangle GetBounds(ObjectEntry obj)
		{
			var radians = 0.0;
			if ((obj.PropertyValue & 1) == 1)
			{
				radians += Math.PI;
			}
			radians += ((obj.PropertyValue & 12) / 4) * (Math.PI * 2 / 3);
			var xoffset = Math.Cos(radians) * 64.0;
			var yoffset = Math.Sin(radians) * 64.0;

			var bounds = sprites[2].Bounds;
			bounds.Offset(obj.X + (int)xoffset, obj.Y + (int)yoffset);
			return bounds;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			var radians = Math.PI * (obj.PropertyValue & 1);
			radians += ((obj.PropertyValue & 12) / 4) * (Math.PI * 2 / 3);

			//var radians = (Math.PI / 8) * (obj.PropertyValue & 0xf);
			var xoffset = Math.Cos(radians) * 16.0;
			var yoffset = Math.Sin(radians) * 16.0;

			var sprite = new Sprite(sprites[2], (int)(xoffset * 4), (int)(yoffset * 4));

			for (var index = 1; index < 4; index++)
			{
				var x = (int)(xoffset * index);
				var y = (int)(yoffset * index);
				sprite = new Sprite(new Sprite(sprites[1], x, y), sprite);
			}

			return new Sprite(sprite, new Sprite(sprites[0]));
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}
