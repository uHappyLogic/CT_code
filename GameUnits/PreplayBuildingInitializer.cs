﻿using System.Collections.Generic;
using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;

namespace Assets.Scripts.GameUnits
{
	internal class PreplayBuildingInitializer : MonoBehaviour, IInOrderInitializable
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
				gameBuilding.Init();
				gameBuilding.ActorAttributes.Team = Team;
				gameBuilding.CompleteConstruction();
				gameBuilding.OnConstructionComplete();

				BuildingsManager.GetInstance().Add(gameBuilding);
			}

			foreach (var prePlayUnit in PrePlayUnits)
			{
				GameUnit gameUnit = prePlayUnit.GetComponent<GameUnit>();
				gameUnit.ActorAttributes.Team = Team;
				gameUnit.Init();

				UnitsManager.GetInstance().Add(gameUnit);
			}
		}
	}
}
