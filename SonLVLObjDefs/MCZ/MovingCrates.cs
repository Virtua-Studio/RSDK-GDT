using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MCZ
{
	class MovingCrates : ObjectDefinition
	{
		private Sprite[] sprite = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone06"))
			{
				sprite[0] = new Sprite(LevelData.GetSpriteSheet("MCZ/Objects.gif").GetSection(136, 1, 64, 64), -32, -32);
			}
			else
			{
				sprite[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(797, 697, 64, 64), -32, -32);
			}
			
			Sprite[] sprites = new Sprite[3];
			
			sprites[0] = new Sprite(sprite[0]);
			sprites[1] = new Sprite(sprite[0], 0x40, 0x40);
			sprites[2] = new Sprite(sprite[0], -0x40, 0x40);
			
			sprite[1] = new Sprite(sprites);
			
			properties[0] = new PropertySpec("Rotate Direction", typeof(int), "Extended",
				"Which direction these Crates should rotate in.", null, new Dictionary<string, int>
				{
					{ "Clockwise", 0 },
					{ "Counter-Clockwise", 1 }
				},
				(obj) => ((obj.PropertyValue == 0) ? 0 : 1),
				(obj, value) => obj.PropertyValue = (byte)(int)value);
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
			return (subtype == 0) ? "Rotate Clockwise" : "Rotate Counter-Clockwise";
		}

		public override Sprite Image
		{
			get { return sprite[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite[1];
		}
	}
}