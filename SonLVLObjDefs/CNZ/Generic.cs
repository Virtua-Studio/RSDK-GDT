using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// this file just hosts basic renders for single-sprite objects that just need zone folder checks

namespace S2ObjectDefinitions.CNZ
{
	class Bumper : CNZ.Generic
	{
		public override Sprite GetSprite()
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '4':
					return new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(148, 100, 32, 32), -16, -16);
				case '9': // Origins Bounce House Mission - do note, this sheet only exists with Origins datapacks
					return new Sprite(LevelData.GetSpriteSheet("MPZ/Objects2.gif").GetSection(1, 1, 32, 32), -16, -16);
				case 'M':
				default:
					return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(511, 339, 32, 32), -16, -16);
			}
		}
	}
	
	class EggmanBomb : CNZ.Generic
	{
		public override Sprite GetSprite()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
			{
				return new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(145, 172, 8, 8), -4, -4);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(224, 130, 8, 8), -4, -4);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanClaw : CNZ.Generic
	{
		public override Sprite GetSprite()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
			{
				return new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(77, 231, 16, 24), -28, 24);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(134, 154, 16, 24), -28, 24);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class VFlipper : CNZ.Generic
	{
		public override Sprite GetSprite()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
			{
				return new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(101, 157, 16, 48), -8, -24);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(321, 338, 16, 48), -8, -24);
			}
		}
	}
	
	class VPlunger : CNZ.Generic
	{
		public override Sprite GetSprite()
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone04"))
			{
				return new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(147, 34, 22, 56), -11, -28);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(189, 338, 22, 56), -11, -28);
			}
		}
	}
	
	abstract class Generic : ObjectDefinition
	{
		private Sprite sprite;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			sprite = GetSprite();
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
		
		public virtual Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 143, 32, 32), -16, -16));
		}
	}
}