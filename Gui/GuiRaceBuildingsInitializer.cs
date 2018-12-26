using Assets.Scripts.Races;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
	public class GuiRaceBuildingsInitializer : MonoBehaviour
	{
		public Button ButtonToSpawnPrefab;
		public GameObject RacePrefab;

		private void Start()
		{
			var list = RacePrefab.GetComponents<RaceBuildingEntry>();

			//.OrderBy(a => a.Building.GetComponent<GameBuilding>().BuildingAttributes.BuildCost);

			float buttonWidth = ButtonToSpawnPrefab.GetComponent<RectTransform>().rect.width;

			foreach (RaceBuildingEntry entry in list)
			{
				ButtonToSpawnPrefab.GetComponent<FactoryButtonDescriptionController>()
					.Factory = entry.Building;

				ButtonToSpawnPrefab.GetComponent<FactoryButtonDescriptionController>()
					.FactoryBuildPreview = entry.BuildingPreview;

				var inst = Instantiate(ButtonToSpawnPrefab);

				inst.transform.SetParent(transform, false);
			}
		}

		// Update is called once per frame
		private void Update()
		{

		}
	}
}
