using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace S2ObjectDefinitions.Mission
{
	class Buzzer2 : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[3];
		private Sprite[] sprites = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[2];
			
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '1')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("EHZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 1, 48, 16), -24, -8);
				sprites[1] = new Sprite(sheet.GetSection(19, 50, 6, 5), 5, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 256, 48, 16), -24, -8);
				sprites[1] = new Sprite(sheet.GetSection(137, 331, 6, 5), 5, -8);
			}
			
			sprites[0] = new Sprite(sprites);
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"The direction the Buzzer will be facing initially.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Cooldown", typeof(int), "Extended",
				"How long this Buzzer's pauses should be, in frames.", null,
				(obj) => (((V4ObjectEntry)obj).Value0 > 0) ? ((V4ObjectEntry)obj).Value0 : 256,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((int)value));
			
			properties[2] = new PropertySpec("Speed", typeof(decimal), "Extended",
				"The speed at which this Buzzer should move horizontally, in pixels per frame.", null,
				(obj) => (decimal)((((V4ObjectEntry)obj).Value1 > 1) ? ((V4ObjectEntry)obj).Value1 : 100),
				(obj, value) => ((V4ObjectEntry)obj).Value1 = (int)(((decimal)value) * 100m));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
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
			return ((subtype & 1) == 0) ? "Facing Left" : "Facing Right";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 1];
		}
	}
}