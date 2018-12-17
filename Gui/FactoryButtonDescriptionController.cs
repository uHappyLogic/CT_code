using System;
using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
	public class FactoryButtonDescriptionController : MonoBehaviour, IInOrderInitializable
	{
		public GameObject Factory;
		public GameObject FactoryBuildPreview;
		public TerrainPointerController TerrainPointerController;
		public BuildingAttributes BuildingAttributes { get; private set; }

		public void InitInOrder()
		{
			_button = GetComponent<Button>();
			_gameUnitFactory = Factory.GetComponent<GameUnitFactory>();
			BuildingAttributes = _gameUnitFactory.GetComponent<BuildingAttributes>();
			_unitAttributes = _gameUnitFactory.PrefabToSpawn.GetComponent<UnitAttributes>();

			BuildingAttributes.Team = Team.TEAM_GAME;
			_unitAttributes.Team = Team.TEAM_GAME;

			_button.GetComponentInChildren<Text>().text = GetDescription();

			_button.onClick.AddListener(OnClick);
		}

		public void OnClick()
		{
			Debug.Log("Button clicked");

			if (TerrainPointerController.transform.childCount > 0)
			{
				Debug.Log("Detaching children");

			}
			else
			{
				Debug.Log("Attaching children");

				GameObject instantiatedPrefab = Instantiate(FactoryBuildPreview);
				instantiatedPrefab.GetComponent<BuildOnClick>().BuildingToBuild = Factory;
				instantiatedPrefab.GetComponent<BuildOnClick>().TerrainPointer = TerrainPointerController;

				TerrainPointerController.AttachObject(instantiatedPrefab, TerrainPointerController.GridAllignementOption.ALLIGN_TO_GRID);
			}
		}

		private string GetDescription()
		{
			StringBuilder content = new StringBuilder();
			content
				.Append(BuildingAttributes.ActorTypeName).Append("\n")
				.Append(String.Format("Cost : {0}\n", BuildingAttributes.BuildCost))
				.Append(String.Format("Unit type : {0}\n", _unitAttributes.ActorTypeName))
				.Append(String.Format("HP : {0}\n", _unitAttributes.HealthPoints))
				.Append(String.Format("AP : {0}\n", _unitAttributes.AttackPoints))
				.Append(String.Format("Range : {0}\n", _unitAttributes.AttackRange))
			;

			return content.ToString();
		}

		private UnitAttributes _unitAttributes;
		private GameUnitFactory _gameUnitFactory;
		private Button _button;
	}
}