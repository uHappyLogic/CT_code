using Assets.Scripts.GameUnits;
using Assets.Scripts.Gui;
using UnityEngine;

namespace Assets.Scripts.Core
{
	public class OrderedInitializer : MonoBehaviour
	{
		[SerializeField]
		public GameObject Gui;

		[SerializeField]
		public GameObject Statistics;

		[SerializeField]
		public UpdateablesManager UpdateablesManager;

		public void Start()
		{
			VisibleLogger.GetInstance().LogDebug("Initializing in order");

			UpdateablesManager.Add(UnitsManager.GetInstance());
			UpdateablesManager.Add(BuildingsManager.GetInstance());

			foreach (var preplayBuildingInitializer in GetComponents<PreplayBuildingInitializer>())
			{
				preplayBuildingInitializer.InitInOrder();
				Destroy(preplayBuildingInitializer);
			}

			foreach (var initializableChild in Gui.GetComponentsInChildren<IInOrderInitializable>())
			{
				initializableChild.InitInOrder();
			}

			VisibleLogger.GetInstance().LogDebug("Initializing in order successfully finished");
		}
	}
}
