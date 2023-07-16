using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Mission
{
	class SetRingCount : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			Sprite frame = new Sprite(LevelData.GetSpriteSheet("Global/Items.gif").GetSection(1, 1, 16, 16), -8, -8);
			
			BitmapBits bitmap = new BitmapBits(18, 18);
			bitmap.DrawRectangle(6, 0, 0, 17, 17); // LevelData.ColorWhite
			sprite = new Sprite(frame, new Sprite(bitmap, -9, -9));
			
			properties[0] = new PropertySpec("Ring Count", typeof(int), "Extended",
				"How many rings the player will start with.", 10,
				(obj) => ((V4ObjectEntry)obj).Value0,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((int)value));
		}
		
		public override bool Debug
		{
			get { return true; }
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
			return subtype + "";
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
	}
}