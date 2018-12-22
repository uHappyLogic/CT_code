using Assets.Scripts.GameUnits;
using UnityEngine;

namespace Assets.Scripts.Core
{
	internal class OrderedInitializer : MonoBehaviour
	{
		[SerializeField]
		public GameObject GUI;

		[SerializeField]
		public GameObject Statistics;
		
		[SerializeField]
		public UpdateablesManager UpdateablesManager;

		public void Start()
		{
			Debug.Log("Initializing in order");

			UpdateablesManager.Add(UnitsManager.GetInstance());
			UpdateablesManager.Add(BuildingsManager.GetInstance());

			foreach (var preplayBuildingInitializer in GetComponents<PreplayBuildingInitializer>())
			{
				preplayBuildingInitializer.InitInOrder();
				Destroy(preplayBuildingInitializer);
			}

			foreach (var initializableChild in GUI.GetComponentsInChildren<IInOrderInitializable>())
			{
				initializableChild.InitInOrder();
			}

			Debug.Log("Initializing in order successfully finished");
		}
	}
}
