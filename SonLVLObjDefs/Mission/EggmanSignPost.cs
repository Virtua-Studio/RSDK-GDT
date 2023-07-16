using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace S2ObjectDefinitions.Mission
{
	class EggmanSignPost : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[4];

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 143, 32, 32), -16, -16);
			
			// these names are kinda bad but hopefully they're understandable enough...
			
			properties[0] = new PropertySpec("Inhale Time", typeof(int), "Extended",
				"How long the Eggman should spend sucking in chemicals.", null,
				(obj) => Math.Min(Math.Max(((V4ObjectEntry)obj).Value0, 0), 18),
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((int)value));
			
			properties[1] = new PropertySpec("Charge Decrease", typeof(int), "Extended",
				"How many less cycles Eggman will inhale chemicals before it reaches full", null,
				(obj) => Math.Min(Math.Max(((V4ObjectEntry)obj).Value1, 0), 12),
				(obj, value) => ((V4ObjectEntry)obj).Value1 = ((int)value));
			
			properties[2] = new PropertySpec("Dropper Speed", typeof(int), "Extended",
				"The speed, in pixels, at which Eggman should follow the player while holding chemicals.", null,
				(obj) => Math.Max(((V4ObjectEntry)obj).Value2, 0) + 1,
				(obj, value) => ((V4ObjectEntry)obj).Value2 = Math.Max(((int)value - 1), 0));
			
			properties[3] = new PropertySpec("Base Speed", typeof(int), "Extended",
				"The speed at which Eggman should cross the arena with, in pixels.", null,
				(obj) => Math.Max(((V4ObjectEntry)obj).Value3, 0) + 3,
				(obj, value) => ((V4ObjectEntry)obj).Value3 = Math.Max(((int)value - 3), 0));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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
			return null;
		}

		public override Sprite Image
		{
			get { return SubtypeImage(0); }
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