using Assets.Scripts.GameUnits;
using UnityEngine;

namespace Assets.Scripts.Core
{
	internal class OrderedInitializer : MonoBehaviour
	{
		public GameObject GUI;
		public GameObject Statistics;

		public void Start()
		{
			Debug.Log("Initializing in order");

			UpdateablesManager updateablesManager = GetComponent<UpdateablesManager>();
			updateablesManager.InitInOrder();

			new BuildingsManager().InitInOrder();
			new UnitsManager().InitInOrder();

			Statistics.GetComponent<StrictInitializedMonoBehaviour>().InitInOrder();

			updateablesManager.Add(UnitsManager.GetInstance());
			updateablesManager.Add(BuildingsManager.GetInstance());

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

			// Destroy(gameObject);
		}
	}
}
