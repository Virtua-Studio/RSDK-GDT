using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class FPlatform : MPZ.Platform
	{
		public override Point GetOffset() { return new Point(0, 0); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 62);
			for (int i = 0; i < 62; i += 12)
				overlay.DrawLine(6, 0, i, 0, i + 6); // LevelData.ColorWhite
			return new Sprite(overlay, 0, 0);
		}
		
		public override PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[1];
			props[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Platform should act upon player contact.", null, new Dictionary<string, int>
				{
					{ "Fall", 0 },
					{ "Static", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			return props;
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Fall" : "Static";
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue > 0) ? null : base.GetDebugOverlay(obj);
		}
	}
	
	class HPlatform : MPZ.Platform
	{
		public override Point GetOffset() { return new Point(-63, 0); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(64, 2);
			overlay.DrawLine(6, 0, 0, 63, 0); // LevelData.ColorWhite
			return new Sprite(overlay, -63, 0);
		}
	}
	
	class HPlatform2 : MPZ.Platform
	{
		public override Point GetOffset() { return new Point(-127, 0); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(128, 2);
			overlay.DrawLine(6, 0, 0, 127, 0); // LevelData.ColorWhite
			return new Sprite(overlay, -127, 0);
		}
	}
	
	class VPlatform : MPZ.Platform
	{
		public override Point GetOffset() { return new Point(0, -63); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 64);
			overlay.DrawLine(6, 0, 0, 0, 63); // LevelData.ColorWhite
			return new Sprite(overlay, 0, -63);
		}
	}
	
	class VPlatform2 : MPZ.Platform
	{
		public override Point GetOffset() { return new Point(0, -127); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 128);
			overlay.DrawLine(6, 0, 0, 0, 127); // LevelData.ColorWhite
			return new Sprite(overlay, 0, -127);
		}
	}
	
	class EPlatform : MPZ.Platform
	{
		public override Point GetOffset() { return new Point(0, 0); }
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 226);
			overlay.DrawLine(6, 0, 0, 0, 225); // LevelData.ColorWhite
			return new Sprite(overlay);
		}
		
		public override PropertySpec[] SetupProperties()
		{
			return null;
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
		}
	}
	
	abstract class Platform : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite[] sprites = new Sprite[2];
		private Sprite debug;
		
		public virtual Point GetOffset() { return new Point(0, 0); }
		public virtual Sprite SetupDebugOverlay() { return null; }
		
		public virtual PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[1];
			props[0] = new PropertySpec("Flip Movement", typeof(bool), "Extended",
				"Whether or not this Platform's movement cycle should be flipped or not.", null,
				(obj) => (obj.PropertyValue == 1),
				(obj, value) => obj.PropertyValue = (byte)(((bool)value) ? 1 : 0));
			
			return props;
		}
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MPZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(383, 207, 64, 24), -32, -12);
			sprites[1] = new Sprite(sprites[0], GetOffset());
			
			debug = SetupDebugOverlay();
			properties = SetupProperties();
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
			return (subtype == 1) ? "Normal Movement" : "Inverted Movement";
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
			return sprites[(obj.PropertyValue == 1) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}