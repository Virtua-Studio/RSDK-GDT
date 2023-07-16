using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.WFZ
{
	class WindCurrent : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Child Type", typeof(int), "Extended",
				"Which object holds the other corner of this Wind Current box.", null, new Dictionary<string, int>
				{
					{ "Next Slot", 0 },
					{ "Previous Slot", 1 }
				},
				(obj) => (obj.PropertyValue > 0) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			try
			{
				ObjectEntry otherObj = LevelData.Objects[LevelData.Objects.IndexOf(obj) + ((obj.PropertyValue == 0) ? 1 : -1)];
				
				short xmin = Math.Min(obj.X, otherObj.X);
				short ymin = Math.Min(obj.Y, otherObj.Y);
				short xmax = Math.Max(obj.X, otherObj.X);
				short ymax = Math.Max(obj.Y, otherObj.Y);
				BitmapBits bmp = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
				
				bmp.DrawRectangle(6, 0, 0, xmax - xmin, ymax - ymin); // LevelData.ColorWhite
				
				return new Sprite(bmp, xmin - obj.X, ymin - obj.Y);
			}
			catch
			{
			}
			
			return null;
		}
	}
}