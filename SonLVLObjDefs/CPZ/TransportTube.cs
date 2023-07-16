using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class TransportTube : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;
		// private Sprite[] debug = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 143, 32, 32), -16, -16);
			
			properties[0] = new PropertySpec("Entry Type", typeof(int), "Extended",
				"desc A.", null, new Dictionary<string, int>
				{
					{ "Entry", 0 },
					{ "Entry 1", 1 },
					{ "Entry 2", 2 },
					{ "Entry 3", 3 },
					{ "Transporter", 0xff }
				},
				(obj) => ((obj.PropertyValue == 0xff) ? (obj.PropertyValue) : (obj.PropertyValue & 3)),
				(obj, value) => {
						// based off RE2's Transport Tube edit scripts
						byte val = (byte)((int)value);
						if (val == 0xff) obj.PropertyValue = val;
						else {
							if (obj.PropertyValue == 0xff)
							{
								// was a transporter, let's reset prop val
								obj.PropertyValue = 0;
							}
							else {
								// not transporter, keep our path value
								obj.PropertyValue &= (byte)(obj.PropertyValue & ~3);
							}
							
							obj.PropertyValue |= val;
						}
					}
				);
			
			properties[1] = new PropertySpec("Path", typeof(int), "Extended",
				"Which path this Tube should follow. Only used by Entry tubes.", null,
				(obj) => ((obj.PropertyValue == 0xff) ? 0 : ((obj.PropertyValue >> 2) & 0x0f)),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue == 0xff) ? obj.PropertyValue : ((obj.PropertyValue & ~(0x0f << 2)) | ((int)value & 0x0f) << 2))); // ignore for Transporters
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
		
		/*
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			// TODO:
			// yeahhhh... this one's gonna be fun
		}
		*/
	}
}