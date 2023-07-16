using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HPZ
{
	class VBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("HPZ/Objects.gif").GetSection(353, 1, 64, 64), -32, -32);
			
			// tagging this area with LevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(65, 193);
			bitmap.DrawLine(6, 32, 32, 32, 160); // movement line
			bitmap.DrawRectangle(6, 0, 0, 63, 63); // top box
			bitmap.DrawRectangle(6, 0, 128, 63, 63); // bottom box
			debug = new Sprite(bitmap, -32, -96);
			
			properties[0] = new PropertySpec("Starting Direction", typeof(int), "Extended",
				"Which direction the Vertical Block will travel in.", null, new Dictionary<string, int>
				{
					{ "Downwards", 0 },
					{ "Upwards", 1 }
				},
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return ((subtype == 1) ? "Start Upwards" : "Start Downwards");
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