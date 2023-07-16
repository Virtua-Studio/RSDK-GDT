using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.HPZ
{
	class TubePath : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Marker Type", typeof(int), "Extended",
				"If this Tube Path is an enterance or an exit.", null, new Dictionary<string, int>
				{
					{ "Enterance", 0 },
					{ "Exit", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = ((byte)((int)value)));
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
			switch (subtype)
			{
				case 0:
					return "Enterance";
				case 1:
					return "Exit";
				default:
					return "Unknown";
			}
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
			{
				// p much copied directly from SCD UFO but hey, if it works it works
				// Do note, in-game behaviour doesn't enforce that the path ends with a Tube Path exit - it just has to be a non-zero obj type
				
				List<ObjectEntry> nodes = LevelData.Objects.Skip(LevelData.Objects.IndexOf(obj) + 1).TakeWhile(a => a.Name == "Blank Object").ToList();
				if (nodes.Count == 0)
					return null;
				
				// Add one forward of final object to list
				// (There's probably a better way to do this, don't look at me for that though!)
				nodes.Add(LevelData.Objects[LevelData.Objects.IndexOf(nodes[nodes.Count - 1]) + 1]);
				
				short xmin = Math.Min(obj.X, nodes.Min(a => a.X));
				short ymin = Math.Min(obj.Y, nodes.Min(a => a.Y));
				short xmax = Math.Max(obj.X, nodes.Max(a => a.X));
				short ymax = Math.Max(obj.Y, nodes.Max(a => a.Y));
				BitmapBits bmp = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
				
				if (obj.X != nodes[0].X || obj.Y != nodes[0].Y)
					bmp.DrawLine(6, obj.X - xmin, obj.Y - ymin, nodes[0].X - xmin, nodes[0].Y - ymin); // LevelData.ColorWhite
				
				for (int i = 0; i < nodes.Count - 1; i++)
					bmp.DrawLine(6, nodes[i].X - xmin, nodes[i].Y - ymin, nodes[i + 1].X - xmin, nodes[i + 1].Y - ymin); // LevelData.ColorWhite
				
				return new Sprite(bmp, xmin - obj.X, ymin - obj.Y);
			}
			
			return null;
		}
	}
}