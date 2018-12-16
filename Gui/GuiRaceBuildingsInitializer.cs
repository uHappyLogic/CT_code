﻿using Assets.Scripts.Races;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
	internal class GuiRaceBuildingsInitializer : MonoBehaviour
	{
		public TerrainPointerController TerrainPointerControllerObject;
		public Button ButtonToSpawnPrefab;
		public GameObject RacePrefab;

		private void Start()
		{
			var list = RacePrefab.GetComponents<RaceBuildingEntry>();

				//.OrderBy(a => a.Building.GetComponent<GameBuilding>().BuildingAttributes.BuildCost);

			var nolist = RacePrefab.GetComponent<RaceBuildingEntry>();

			int i = 0;
			float buttonWidth = ButtonToSpawnPrefab.GetComponent<RectTransform>().rect.width;

			foreach (RaceBuildingEntry entry in list)
			{
				ButtonToSpawnPrefab.GetComponent<FactoryButtonDescriptionController>()
					.Factory = entry.Building;

				ButtonToSpawnPrefab.GetComponent<FactoryButtonDescriptionController>()
					.FactoryBuildPreview = entry.BuildingPreview;

				ButtonToSpawnPrefab.GetComponent<FactoryButtonDescriptionController>()
					.TerrainPointerController = TerrainPointerControllerObject;

				var inst = Instantiate(ButtonToSpawnPrefab);

				inst.transform.SetParent(transform, false);
				//inst.transform.localRotation = transform.localRotation;
				//inst.transform.localScale = transform.localScale;
			}
		}

		// Update is called once per frame
		private void Update()
		{

		}
	}
}