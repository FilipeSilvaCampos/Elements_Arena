using System;
using UnityEngine;

namespace ElementsArena.UI
{
	[CreateAssetMenu(fileName = "New Arena", menuName = "Make New Arena")]
	public class ArenaScriptableObject : ScriptableObject
	{
		public string denomination;
		public int buildIndex;
		public Sprite previewImage;
	}
}
