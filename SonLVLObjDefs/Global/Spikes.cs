using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Global
{
	class Spikes : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[4];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items2.gif");
			sprites[0] = new Sprite(sheet.GetSection(182, 99, 32, 32), -16, -16);
			sprites[1] = new Sprite(sheet.GetSection(182, 132, 32, 32), -16, -16);
			sprites[2] = new Sprite(sheet.GetSection(215, 132, 32, 32), -16, -16);
			sprites[3] = new Sprite(sheet.GetSection(215, 99, 32, 32), -16, -16);
			
			properties = new PropertySpec[3];
			properties[0] = new PropertySpec("Orientation", typeof(int), "Extended",
				"Which way the Spikes are facing.", null, new Dictionary<string, int>
				{
					{ "Up", 0 },
					{ "Right", 1 },
					{ "Left", 2 },
					{ "Down", 3 }
				},
				(obj) => (obj.PropertyValue & 3),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 252) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Moving", typeof(int), "Extended",
				"If these Spikes are retracting or not. Their position in the scene is their extended position.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 128 }
				},
				(obj) => (obj.PropertyValue & 128),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 127) | (byte)((int)value)));
			
			properties[2] = new PropertySpec("Parent Offset", typeof(int), "Extended",
				"The entity pos offset of these Spikes' parent. Only applicable if state is set to 5, to be carried by another object.", null,
				(obj) => ((V4ObjectEntry)obj).Value2,
				(obj, value) => ((V4ObjectEntry)obj).Value2 = ((int)value));
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
			return null;
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
			return sprites[obj.PropertyValue & 3];
		}
	}
}