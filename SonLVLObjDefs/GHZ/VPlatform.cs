using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.GHZ
{
	class VPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("GHZ/Objects.gif").GetSection(50, 18, 64, 32), -32, -14);
			
			// tagging this area with LevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(65, 129);
			bitmap.DrawRectangle(6, 0, 0, 63, 32); // top box
			bitmap.DrawRectangle(6, 0, 96, 63, 32); // bottom box
			bitmap.DrawLine(6, 32, 10, 32, 106); // movement line
			debug = new Sprite(bitmap, -32, -58);
			
			properties[0] = new PropertySpec("Start Direction", typeof(int), "Extended",
				"The starting direction of this Platform.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Downwards", 1 }
				},
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
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
			return (subtype == 1) ? "Start Downwards" : "Start Upwards";
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
			return debug;
		}
	}
}