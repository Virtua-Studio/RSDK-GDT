using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.EHZ
{
	class Waterfall : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[10];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("EHZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(192, 0, 64, 16), -32, -128);
				sprites[1] = new Sprite(sheet.GetSection(192, 0, 64, 256), -32, -128);
				sprites[2] = new Sprite(sheet.GetSection(1, 1, 1, 1), 0, 0);
				sprites[3] = new Sprite(sheet.GetSection(192, 16, 64, 64), -32, -32);
				sprites[4] = new Sprite(sheet.GetSection(1, 1, 1, 1), 0, 0);
				sprites[5] = new Sprite(sheet.GetSection(192, 16, 64, 160), -32, -64);
				sprites[6] = new Sprite(sheet.GetSection(192, 0, 64, 16), -32, -128);
				sprites[7] = new Sprite(sheet.GetSection(192, 0, 64, 192), -32, -128);
				sprites[8] = new Sprite(sheet.GetSection(192, 64, 64, 96), -32, -32);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 435, 64, 16), -32, -128);
				sprites[1] = new Sprite(sheet.GetSection(1, 435, 64, 256), -32, -128);
				sprites[2] = new Sprite(sheet.GetSection(1, 1, 1, 1), 0, 0);
				sprites[3] = new Sprite(sheet.GetSection(1, 451, 64, 64), -32, -32);
				sprites[4] = new Sprite(sheet.GetSection(1, 1, 1, 1), 0, 0);
				sprites[5] = new Sprite(sheet.GetSection(1, 451, 64, 160), -32, -64);
				sprites[6] = new Sprite(sheet.GetSection(1, 435, 64, 16), -32, -128);
				sprites[7] = new Sprite(sheet.GetSection(1, 435, 64, 192), -32, -128);
				sprites[8] = new Sprite(sheet.GetSection(1, 499, 64, 96), -32, -32);
			}
			
			// Set up the debug visualisation for when the current frame is an empty sprite
			BitmapBits bitmap = new BitmapBits(65, 65);
			bitmap.DrawRectangle(6, 0, 0, 64, 64); // LevelData.ColorWhite
			sprites[9] = new Sprite(bitmap, -32, -32);
			
			properties[0] = new PropertySpec("Frame", typeof(int), "Extended",
				"Which frame this Waterfall should display.", null, new Dictionary<string, int>
				{
					{ "Frame 0", 0 }, // yeah these names are kinda bad and don't make much sense but i don't got anything better, so
					{ "Frame 1", 1 },
					{ "Frame 3", 3 },
					{ "Frame 5", 5 },
					{ "Frame 6", 6 },
					{ "Frame 7", 7 },
					{ "Frame 8", 8 }
				},
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 3, 5, 6, 7, 8 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "Frame " + (subtype);
		}

		public override Sprite Image
		{
			get { return sprites[1]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[Math.Min((int)subtype, 9)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[Math.Min((int)obj.PropertyValue, 9)];
		}
	}
}