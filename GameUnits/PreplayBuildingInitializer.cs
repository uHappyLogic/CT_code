using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;

namespace Assets.Scripts.GameUnits
{
	public class PreplayBuildingInitializer : MonoBehaviour, IInOrderInitializable
	{
		[SerializeField]
		public Team Team;

		[SerializeField]
		public List<GameObject> PrePlayBuildings;

		[SerializeField]
		public List<GameObject> PrePlayUnits;

		public void InitInOrder()
		{
			foreach (var prePlayBuilding in PrePlayBuildings)
			{
				GameBuilding gameBuilding = prePlayBuilding.GetComponent<GameBuilding>();
				gameBuilding.ActorAttributes.Team = Team;
				gameBuilding.CompleteConstruction();
				gameBuilding.OnConstructionComplete();

				BuildingsManager.GetInstance().Add(gameBuilding);
			}

			foreach (var prePlayUnit in PrePlayUnits)
			{
				GameUnit gameUnit = prePlayUnit.GetComponent<GameUnit>();
				gameUnit.ActorAttributes.Team = Team;

				UnitsManager.GetInstance().Add(gameUnit);
			}
		}
	}
}
