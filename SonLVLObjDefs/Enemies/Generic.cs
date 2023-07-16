using SonicRetro.SonLVL.API;
using System;

// mostly just projeciles and other basic renders which only need MBZ checks

namespace S2ObjectDefinitions.Enemies
{
	class AquisShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
				return new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(99, 18, 8, 8), -4, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(933, 323, 8, 8), -4, -4);
		}
	}
	
	class Asteron : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
				return new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(223, 1, 32, 28), -16, -14);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(843, 289, 32, 28), -16, -14); // (SCZ mission ends up here too)
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class AsteronSpike : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
				return new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(182, 1, 7, 14), -4, -8);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(968, 322, 7, 14), -4, -8); // (SCZ mission ends up here too)
		}
	}
	
	class Ball : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(166, 1, 24, 24), -12, -12);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(166, 1, 24, 24), -12, -12); // broken frame btw
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class BigTurtloid : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone10"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(72, 42, 56, 31), -28, -15);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(72, 42, 56, 31), -28, -15); // broken frame btw
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class Bubbler : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(190, 141, 14, 14), 2, -2);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(166, 1, 24, 24), -12, -12); // broken frame btw
		}
	}
	
	class BuzzerShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(1, 50, 8, 10), -12, -3);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(66, 302, 8, 10), -12, -3);
		}
	}
	
	class Clucker : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[2];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("SCZ/Objects.gif");
				sprites[1] = new Sprite(sheet.GetSection(9, 223, 32, 32), -16, -16);
				sprites[0] = new Sprite(sheet.GetSection(1, 246, 8, 7), -24, 7);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[1] = new Sprite(sheet.GetSection(845, 256, 32, 32), -16, -16);
				sprites[0] = new Sprite(sheet.GetSection(837, 279, 8, 7), -24, 7);
			}
			
			return new Sprite(sprites);
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class CluckerBase : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(1, 206, 48, 16), -24, -8);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(576, 289, 48, 16), -24, -8);
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class CluckerShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone11"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(50, 214, 6, 8), -3, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(660, 289, 6, 8), -3, -4);
		}
	}
	
	class Coconut : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(82, 95, 12, 13), -6, -7);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(258, 297, 12, 13), -6, -7);
		}
	}
	
	class Coconuts : Enemies.Generic
	{
		// Coconuts' subtype is normally 30, but this value isn't used in any proper way
		// (property value is treated as direction, but it gets reset to face the player in-game and a subtype of 30 doesn't exactly sound like a direction either)
		// MBZ Coconuts don't have anything in their prop val either anyways, so let's just gloss over it here
		
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(1, 63, 26, 45), -8, -14);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(50, 256, 26, 45), -8, -14);
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class Crawl : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
				return new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(1, 1, 47, 32), -23, -16);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(436, 256, 47, 32), -23, -16);
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class Crawlton : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[2];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone06"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MCZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(152, 114, 24, 16), -16, -8);
				sprites[1] = new Sprite(sheet.GetSection(135, 114, 16, 15), -8, -8);
			}
			else
			{
				// broken frames btw
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(152, 114, 24, 16), -16, -8);
				sprites[1] = new Sprite(sheet.GetSection(135, 114, 16, 15), -8, -8);
			}
			
			return new Sprite(sprites);
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class Flasher : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone06"))
				return new Sprite(LevelData.GetSpriteSheet("MCZ/Objects.gif").GetSection(1, 1, 23, 15), -16, -8);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 1, 23, 15), -16, -8); // broken frame btw
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class GrabberShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(46, 83, 8, 8), -4, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(84, 302, 8, 8), -4, -4);
		}
	}
	
	class Masher : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone01"))
				return new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(105, 67, 20, 32), -10, -16);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(66, 313, 20, 32), -10, -16);
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class NebulaBomb : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone10"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(220, 1, 14, 13), -7, -7);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(220, 1, 14, 13), -7, -7);  // broken frame btw
		}
	}
	
	class OctusShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
				return new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(92, 32, 6, 6), -3, -3);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(910, 332, 6, 6), -3, -3); // (SCZ mission ends up here too)
		}
	}
	
	class Rexon : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[3];
			
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(143, 123, 23, 16), -19, -6); // head
				sprites[1] = new Sprite(sheet.GetSection(52, 38, 16, 16), -8, -8); // body piece
				sprites[2] = new Sprite(sheet.GetSection(91, 105, 32, 16), -16, -8); // shell
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				
				// broken frames btw
				sprites[0] = new Sprite(sheet.GetSection(143, 123, 23, 16), -19, -6); // head
				sprites[1] = new Sprite(sheet.GetSection(52, 38, 16, 16), -8, -8); // body piece
				sprites[2] = new Sprite(sheet.GetSection(91, 105, 32, 16), -16, -8); // shell
			}
			
			// Assemble the Rexon sprite now
			
			int[] offsets = {
				-31,  4,  // piece 4, spr 1
				-29, -11, // piece 3, spr 1
				-25, -25, // piece 2, spr 1
				-20, -39, // piece 1, spr 1
				-16, -54, // head, spr 0
				 0,   0   // shell, spr 2
			};
			
			Sprite[] sprs = new Sprite[6];
			for (int i = 0; i < 6; i++)
				sprs[i] = new Sprite(sprites[((i == 5) ? 2 : (i == 4) ? 0 : 1)], offsets[i * 2], offsets[(i * 2) + 1]);
			
			return new Sprite(sprs);
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class RexonShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone05"))
				return new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(36, 54, 8, 8), -4, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(36, 54, 8, 8), -4, -4); // broken frame btw
		}
	}
	
	class SlicerArm : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone09"))
				return new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(75, 51, 16, 16), 0, -16);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(926, 306, 16, 16), 0, -16);
		}
	}
	
	class SmallTurtloid : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone10"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(186, 42, 24, 23), -12, -11);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(186, 42, 24, 23), -12, -11); // broken frame btw
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	class SpinyShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone02"))
				return new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(73, 25, 8, 8), -4, -4);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(84, 302, 8, 8), -4, -4);
		}
	}
	
	class TurtloidShot : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone10"))
				return new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(220, 29, 6, 6), -3, -3);
			else
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(815, 310, 6, 6), -3, -3); // broken frame btw
		}
	}
	
	class Whisp : Enemies.Generic
	{
		public override Sprite GetFrame()
		{
			Sprite[] sprites = new Sprite[2];
			if (LevelData.StageInfo.folder.EndsWith("Zone03"))
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(34, 42, 24, 15), -12, -7);
				sprites[1] = new Sprite(sheet.GetSection(34, 58, 21, 6), -9, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(111, 317, 24, 15), -12, -7);
				sprites[1] = new Sprite(sheet.GetSection(110, 302, 21, 6), -9, -8);
			}
			
			return new Sprite(sprites);
		}
		
		public override bool Hidden { get { return false; } }
	}
	
	abstract class Generic : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
		}
		
		public override System.Collections.ObjectModel.ReadOnlyCollection<byte> Subtypes
		{
			get { return new System.Collections.ObjectModel.ReadOnlyCollection<byte>(new System.Collections.Generic.List<byte>()); }
		}
		
		public override bool Hidden
		{
			get { return true; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "";
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
		
		public virtual Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 143, 32, 32), -16, -16);
		}
	}
}