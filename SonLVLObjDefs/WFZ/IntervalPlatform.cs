using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class TurretPlatform : WFZ.IntervalPlatform
	{
		public override Sprite GetSprite()
		{
			return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(130, 165, 64, 24), -32, -24);
		}
	}
	
	class TiltPlatformH : WFZ.IntervalPlatform
	{
		public override Sprite GetSprite()
		{
			return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(320, 99, 48, 8), -24, -4);
		}
	}
	
	class TiltPlatformV : WFZ.IntervalPlatform
	{
		public override Sprite GetSprite()
		{
			return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(311, 99, 8, 48), -4, -24);
		}
	}
	
	class TiltPlatformL : WFZ.IntervalPlatform
	{
		public override Sprite GetSprite()
		{
			return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(320, 99, 48, 8), -24, -4);
		}
	}
	
	// TiltPlatformM doesn't use its prop val
	
	abstract class IntervalPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public virtual Sprite GetSprite() { return null; }
		
		public override void Init(ObjectData data)
		{
			sprite = GetSprite();
			
			properties[0] = new PropertySpec("Interval Offset", typeof(int), "Extended",
				"The interval offset this Platform should use.", null,
				(obj) => obj.PropertyValue >> 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0xF0) | (byte)((int)value << 4)));
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
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
	}
}