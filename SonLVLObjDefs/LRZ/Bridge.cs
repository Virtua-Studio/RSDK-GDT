using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HPZ
{
	class Bridge : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("LRZ/Objects.gif").GetSection(68, 1, 16, 16), -8, -8);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 8, 10, 12, 14, 16 }); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 8; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype) + " logs";
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int st = -(((obj.PropertyValue) * 16) / 2) + 8;
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < (obj.PropertyValue); i++)
			{
				Sprite tmp = new Sprite(img);
				tmp.Offset(st + (i * 16), 0);
				sprs.Add(tmp);
			}
			return new Sprite(sprs.ToArray());
		}
	}
}