using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S2ObjectDefinitions.Mission
{
	class M035Block : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("Mission/Objects.gif").GetSection(1, 18, 32, 32), -16, -16);
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(6, 0, 0, 31, 31); // LevelData.ColorWhite
			sprites[1] = new Sprite(bitmap, -16, -16);
			
			properties[0] = new PropertySpec("Child Type", typeof(int), "Extended",
				"Which object slot this Block should draw between. The first Block sould be on the left, while the second Block should be on the right.", null, new Dictionary<string, int>
				{
					{ "Next Slot", 0 },
					{ "Previous Slot", 1 }
				},
				(obj) => (obj.PropertyValue > 0) ? 1 : 0, // should just be 0/1 with other values unsupported, but let's turn nonzeroes into ones here
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
			// weird names but can't think of anything better, so
			switch (subtype)
			{
				case 0: return "Left Side, Next Slot";
				case 1: return "Right Side, Previous Slot";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}
		
		// Kind of hacky and definitely a really really bad solution... there's almost certainly a better way to do this but i can't think of anything better so this'll have to do
		// (this is here to avoid an unlimited recursion loop when two blocks repeatedly call each other's UpdateSprite())
		int spriteUpdateCount = 0;
		
		public override Sprite GetSprite(ObjectEntry obj)
		{
			try
			{
				if (obj.PropertyValue == 0)
				{
					ObjectEntry other = LevelData.Objects[LevelData.Objects.IndexOf(obj) + 1];
					if (other.Name != "M035Block" || other.PropertyValue != 1)
					{
						spriteUpdateCount = 0;
						return sprites[0];
					}
					else
					{
						if (spriteUpdateCount == 0)
						{
							spriteUpdateCount++;
							other.UpdateSprite();
						}
						else
							spriteUpdateCount = 0;
						
						Sprite sprite = new Sprite();
						for (int i = obj.X; i < other.X; i += 32)
							sprite = new Sprite(sprite, new Sprite(sprites[0], i - obj.X, 0));
						return sprite;
					}
				}
				else
				{
					ObjectEntry other = LevelData.Objects[LevelData.Objects.IndexOf(obj) - 1];
					if (other.Name != "M035Block" || other.PropertyValue != 0)
					{
						spriteUpdateCount = 0;
						return sprites[0];
					}
					else
					{
						if (spriteUpdateCount == 0)
						{
							spriteUpdateCount++;
							other.UpdateSprite();
						}
						else
							spriteUpdateCount = 0;
						Sprite sprite = new Sprite();
						for (int i = other.X; i < obj.X; i += 32)
							sprite = new Sprite(sprite, new Sprite(sprites[0], i - other.X, 0));
						sprite.Offset(other.X - obj.X, other.Y - obj.Y);
						return sprite;
					}
				}
			}
			catch
			{
			}
			
			spriteUpdateCount = 0;
			return sprites[0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			/*
			try
			{
				if (obj.PropertyValue == 0)
				{
					ObjectEntry other = LevelData.Objects[LevelData.Objects.IndexOf(obj) + 1];
					if (other.Name == "M035Block" && other.PropertyValue == 1 && other.X > obj.X) return null;
				}
				else
				{
					ObjectEntry other = LevelData.Objects[LevelData.Objects.IndexOf(obj) - 1];
					if (other.Name == "M035Block" && other.PropertyValue == 0 && other.X < obj.X) return null;
				}
			}
			catch
			{
			}
			*/
			return sprites[1];
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			return new Rectangle(obj.X - 16, obj.Y - 16, 32, 32);;
		}
	}
}