using Assets.Scripts.Multi;
using UnityEngine;

namespace Assets.Scripts.Utils
{
	public static class GameObjectExtensionsMethods
	{
		public static string GetStaticAssetUniqueId(this GameObject gameObject)
		{
			return gameObject.GetComponent<GameObjectGuidIdentificator>().Guid;
		}
	}
}
