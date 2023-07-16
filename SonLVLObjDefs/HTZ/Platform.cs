using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HTZ
{
	class HPlatform : HTZ.Platform
	{
		public override Point Offset { get { return new Point(64, 0); } }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits bitmap = new BitmapBits(129, 2);
			bitmap.DrawLine(6, 0, 0, 128, 0); // LevelData.ColorWhite
			return new Sprite(bitmap, -64, 0);
		}
	}
	
	class VPlatform : HTZ.Platform
	{
		public override Point Offset { get { return new Point(0, 64); } }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 129);
			overlay.DrawLine(6, 0, 0, 0, 128); // LevelData.ColorWhite
			return new Sprite(overlay, 0, -64);
		}
	}
	
	class VPlatform2 : HTZ.Platform
	{
		public override Point Offset { get { return new Point(0, 32); } }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 65);
			overlay.DrawLine(6, 0, 0, 0, 64); // LevelData.ColorWhite
			return new Sprite(overlay, 0, -32);
		}
		
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(191, 126, 64, 96), -32, -52);
		}
	}
	
	abstract class Platform : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[3];
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public virtual Point Offset { get { return new Point(0, 0); } }
		public virtual Sprite SetupDebugOverlay() { return null; }
		
		public virtual Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(191, 223, 64, 32), -32, -12);
		}
		
		public override void Init(ObjectData data)
		{
			sprites[2] = GetFrame();
			sprites[0] = new Sprite(sprites[2],  Offset.X,  Offset.Y);
			sprites[1] = new Sprite(sprites[2], -Offset.X, -Offset.Y);
			
			debug = SetupDebugOverlay();
			properties[0] = new PropertySpec("Flip Movement", typeof(bool), "Extended",
				"If this Platform's movement cycle should be flipped or not.", null,
				(obj) => (obj.PropertyValue == 1),
				(obj, value) => obj.PropertyValue = (byte)((bool)value ? 1 : 0));
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
			return (subtype == 1) ? "Flipped Movement" : "Normal Movement";
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 1) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}