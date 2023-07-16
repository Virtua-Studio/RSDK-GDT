using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Special
{
	class StartMessage : Special.Checkpoint
	{
		public override Sprite GetSprite()
		{
			return new Sprite(LevelData.GetSpriteSheet("Special/Objects.gif").GetSection(367, 91, 144, 30), -72, -15); // start frame
		}
	}
	
	class Checkpoint : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[3];

		public override void Init(ObjectData data)
		{
			sprite = GetSprite();
			
			// TODO: i think it'd be cool to make all these ring counts into a dropdown like Position or Advanced Properties
			// but then if i do, the values won't immediately show, and the user might not even know they're editable...
			
			properties[0] = new PropertySpec("Ring Count - 2P", typeof(int), "Extended",
				"How many rings the player should need to past the next checkpoint when two players are active.", null,
				(obj) => ((V4ObjectEntry)obj).Value0,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = (int)value);
			
			properties[1] = new PropertySpec("Ring Count - ST", typeof(int), "Extended",
				"How many rings the player should need to past the next checkpoint if the current player is just Sonic/Tails solo.", null,
				(obj) => ((V4ObjectEntry)obj).Value1,
				(obj, value) => ((V4ObjectEntry)obj).Value1 = (int)value);
			
			properties[2] = new PropertySpec("Ring Count - K", typeof(int), "Extended",
				"How many rings the player should need to past the next checkpoint if the current player is Knuckles solo.", null,
				(obj) => ((V4ObjectEntry)obj).Value2,
				(obj, value) => ((V4ObjectEntry)obj).Value2 = (int)value);
		}
		
		public virtual Sprite GetSprite()
		{
			return new Sprite(LevelData.GetSpriteSheet("Special/Objects.gif").GetSection(199, 165, 32, 16), -16, -8); // checkpoint sprite
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