using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.MPZ
{
	class BeltActivation : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Activate Count", typeof(int), "Extended",
				"How many of the following objects should be activated by this Activator.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 8; }
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
			if (obj.PropertyValue == 0)
				return null;
			
			// Do note - in tradtional fashion, the Origins folks deleted one Platform while not decreasing
			// its activator's count, so now a random invis block far across the level is activated by this object too in that mission
			// This isn't this render breaking, it's just how it is :(
			
			List<ObjectEntry> objs = LevelData.Objects.Skip(LevelData.Objects.IndexOf(obj)).TakeWhile(a => LevelData.Objects.IndexOf(a) <= (LevelData.Objects.IndexOf(obj) + obj.PropertyValue)).ToList();
			if (objs.Count == 0)
				return null;
			
			short xmin = Math.Min(obj.X, objs.Min(a => a.X));
			short ymin = Math.Min(obj.Y, objs.Min(a => a.Y));
			short xmax = Math.Max(obj.X, objs.Max(a => a.X));
			short ymax = Math.Max(obj.Y, objs.Max(a => a.Y));
			BitmapBits bmp = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
			for (int i = 0; i < objs.Count - 1; i++)
				bmp.DrawLine(6, obj.X - xmin, obj.Y - ymin, objs[i + 1].X - xmin, objs[i + 1].Y - ymin); // LevelData.ColorWhite
			
			return new Sprite(bmp, xmin - obj.X, ymin - obj.Y);
		}
	}
}