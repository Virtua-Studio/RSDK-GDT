using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MCZ
{
	class VDropBridge : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			Sprite img = new Sprite(LevelData.GetSpriteSheet("MCZ/Objects.gif").GetSection(135, 131, 16, 16), -8, -8);
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 8; i++)
				sprs.Add(new Sprite(img, 0, -(i * 16)));
			
			sprite = new Sprite(sprs.ToArray());
			
			BitmapBits bitmap = new BitmapBits(129, 17);
			bitmap.DrawRectangle(6, 0, 0, 128, 16); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, 8, -8);
			debug[1] = new Sprite(bitmap, -136, -8);
			
			properties[0] = new PropertySpec("Open Direction", typeof(int), "Extended",
				"Which direction this Bridge should open towards.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (byte)((int)value)));
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
			return ((subtype & 1) == 0) ? "Open Right" : "Open Left";
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
			return debug[obj.PropertyValue & 1];
		}
	}
}