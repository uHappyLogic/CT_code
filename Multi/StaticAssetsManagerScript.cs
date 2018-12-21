using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Multi
{
	public class StaticAssetsManagerScript : MonoBehaviour
	{
		[SerializeField]
		public List<GameObject> ToRegister;

		public void Start()
		{
			_instance = this;

			mappings = new Dictionary<string, GameObject>();

			foreach (GameObject toRegister in ToRegister)
			{
				mappings[toRegister.GetComponent<GameObjectGuidIdentificator>().Guid] = toRegister;
			}
		}

		public static StaticAssetsManagerScript GetInstance()
		{
			return _instance;
		}

		public GameObject GetRegistered(string guid)
		{
			return mappings[guid];
		}


		private Dictionary<string, GameObject> mappings;

		private static StaticAssetsManagerScript _instance;

	}
}
