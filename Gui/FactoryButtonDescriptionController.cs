using System.Text;
using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;
using Assets.Scripts.Multi;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gui
{
	public class FactoryButtonDescriptionController : MonoBehaviour, IInOrderInitializable
	{
		public GameBuilding Factory;
		public GameObject FactoryBuildPreview;

		public void InitInOrder()
		{
			_button = GetComponent<Button>();
			_gameUnitFactory = Factory.GetComponent<GameUnitFactory>();
			_buildingAttributes = _gameUnitFactory.GetComponent<BuildingAttributes>();
			_unitAttributes = _gameUnitFactory.PrefabToSpawn.GetComponent<UnitAttributes>();

			_buildingAttributes.Team = Team.TEAM_GAME;
			_unitAttributes.Team = Team.TEAM_GAME;

			_button.GetComponentInChildren<Text>().text = GetDescription();

			_button.onClick.AddListener(OnClick);
		}

		public void OnClick()
		{
			VisibleLogger.GetInstance().LogDebug("Button clicked");

			if (TerrainPointerControllerProvider.GetInstance().IsObjectAttached)
			{
				VisibleLogger.GetInstance().LogDebug("Detaching children");
				TerrainPointerControllerProvider.GetInstance().DetachObject();
			}
			else
			{
				if (PlayerScript.GetInstance().Gold < _buildingAttributes.BuildCost)
				{
					VisibleLogger.GetInstance().LogDebug("Not enought money to build");
					return;
				}

				VisibleLogger.GetInstance().LogDebug("Attaching children");
				TerrainPointerControllerProvider.GetInstance().AttachObject(FactoryBuildPreview, TerrainPointerController.GridAllignementOption.ALLIGN_TO_GRID);
			}
		}

		private string GetDescription()
		{
			StringBuilder content = new StringBuilder();
			content
				.Append(_buildingAttributes.ActorTypeName).Append("\n")
				.Append(string.Format("Cost : {0}\n", _buildingAttributes.BuildCost))
				.Append(string.Format("Unit type : {0}\n", _unitAttributes.ActorTypeName))
				.Append(string.Format("HP : {0}\n", _unitAttributes.HealthPoints))
				.Append(string.Format("AP : {0}\n", _unitAttributes.AttackPoints))
				.Append(string.Format("Range : {0}\n", _unitAttributes.AttackRange))
			;

			return content.ToString();
		}

		private UnitAttributes _unitAttributes;
		private GameUnitFactory _gameUnitFactory;
		private Button _button;
		private BuildingAttributes _buildingAttributes;
	}
}