using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class SwingPlatform : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties = new PropertySpec[3];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("OOZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(51, 75, 16, 16), -8, -8); // post and chain are same sprite (original script was copied from ARZ (and so is this def too lol))
			sprites[1] = new Sprite(sheet.GetSection(51, 75, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(68, 75, 64, 32), -32, -8);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How many chains the Platform should hang off of.", null,
				(obj) => (obj.PropertyValue & 0x7f),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 0x80) | (byte)((int)value & 0x7f)));
			
			properties[1] = new PropertySpec("Can Snap", typeof(int), "Extended",
				"If the platform should detach and turn into a raft. Does not work when X-flipped.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 1 }
				},
				(obj) => (obj.PropertyValue >> 7),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 0x7f) | (byte)((int)value << 7)));
			
			properties[2] = new PropertySpec("Inverted", typeof(bool), "Extended",
				"If the Swinging Platform's movement should be inverted, compared to normal Swing Platforms.", null,
				(obj) => (((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)),
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)((bool)value ? 1 : 0));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 4; }
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
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int chains = (obj.PropertyValue & 0x7f);
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i <= (chains + 1); i++)
			{
				int frame = (i == 0) ? 0 : (i == (chains + 1)) ? 2 : 1; // no reason to make the post use a separate frame as a chian when they're the same sprite anyways but may as well, y'know?
				Sprite sprite = new Sprite(sprites[frame]);
				if (i == (chains + 1))
				{
					sprite.Offset(0, (i * 16) - 8);
				}
				else
				{
					sprite.Offset(0, (i * 16));
				}
				sprs.Add(sprite);
			}
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int chains = (obj.PropertyValue & 0x7f);
			BitmapBits overlay = new BitmapBits(2 * ((chains * 16) + 24) + 1, (chains * 16) + 25);
			overlay.DrawCircle(6, ((chains * 16) + 24), 0, (chains * 16) + 8); // LevelData.ColorWhite
			return new Sprite(overlay, -((chains * 16) + 24), 0);
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			var bounds = sprites[2].Bounds;
			bounds.Offset(obj.X, obj.Y + ((obj.PropertyValue & 0x7f) + 1) * 16 - 8);
			return bounds;
		}
	}
}