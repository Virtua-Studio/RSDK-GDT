using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace S2ObjectDefinitions.HPZ
{
	class RotatePlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[4];
		private Sprite[] sprites = new Sprite[6];
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("HPZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(181, 73, 16, 16), -8, -8);
			sprites[1] = new Sprite(sheet.GetSection(181, 90, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(181, 107, 16, 16), -8, -8);
			sprites[3] = new Sprite(sheet.GetSection(181, 73, 16, 16), -8, -8);
			sprites[4] = new Sprite(sheet.GetSection(1, 183, 64, 16), -32, -8);
			sprites[5] = new Sprite(sheet.GetSection(402, 66, 40, 40), -20, -20);
			
			// tagging this area with LevelData.ColorWhite
			
			BitmapBits bitmap = new BitmapBits(161, 161);
			bitmap.DrawCircle(6, 80, 80, 80);
			debug[0] = new Sprite(bitmap, -80, -80);
			
			// TODO: it's kinda half confusing ish what two circles mean, find some way to make it better
			bitmap = new BitmapBits(193, 193);
			bitmap.DrawCircle(6, 96, 96, 96); // Outer circle
			bitmap.DrawCircle(6, 96, 96, 72); // Inner circle
			debug[1] = new Sprite(bitmap, -96, -96);
			
			properties[0] = new PropertySpec("Angle", typeof(int), "Extended",
				"What angle this Platform should start at.", null,
				(obj) => obj.PropertyValue & 7,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~7) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Rotate Speed", typeof(int), "Extended",
				"What speed this Platform should spin at.", null,
				(obj) => (obj.PropertyValue >> 3) & 15,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~120) | (byte)(((int)value & 15) << 3)));
			
			properties[2] = new PropertySpec("Use Spiked Ball", typeof(bool), "Extended",
				"If this object should be a Spiked Ball, as opposed to a Platform.", null,
				(obj) => (obj.PropertyValue & 0x80) == 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~7) | (byte)((bool)value ? 0x80 : 0x00)));
			
			properties[3] = new PropertySpec("Dynamic", typeof(bool), "Extended",
				"If this platform's radius should expand and contract.", null,
				(obj) => (((V4ObjectEntry)obj).State == 1),
				(obj, value) => ((V4ObjectEntry)obj).State = ((bool)value ? 1 : 0));
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
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[4]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[4];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			// yeahhhh this is kinda janky, but if it works it works...
			
			List<Sprite> sprs = new List<Sprite>();
			
			int length = (((V4ObjectEntry)obj).State == 1) ? 5 : 4;
			double angle = ((obj.PropertyValue & 7) * 0x2a)/128.0 * Math.PI;
			// double amplitude = (((V4ObjectEntry)obj).State == 1) ? 0.75 : 1.0;
			
			for (int i = 0; i <= length; i++)
			{
				int frame = (i < length) ? 3 : ((obj.PropertyValue & 0x80) == 0x80) ? 5 : 4;
				sprs.Add(new Sprite(sprites[frame], (int)(Math.Cos(angle) * ((i+1) * 16)), (int)(Math.Sin(angle) * ((i+1) * 16))));
			}
			
			sprs.Add(new Sprite(sprites[2]));
			
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(((V4ObjectEntry)obj).State == 1) ? 1 : 0];
		}
	}
}