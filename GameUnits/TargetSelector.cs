using System.Linq;
using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;
using Assets.Scripts.Gui;
using UnityEngine;

namespace Assets.Scripts.GameUnits
{
	public class TargetSelector
	{
		public static TargetSelector GetInstance()
		{
			if (_targetSelector == null)
			{
				VisibleLogger.GetInstance().LogDebug("Target selector initialized");
				_targetSelector = new TargetSelector();
			}

			return _targetSelector;
		}

		public GameActor GetNearestHostileUnit(GameUnit attacker)
		{
			HostileUnitSearchResult nearestHostileUnit = SearchForNearest(_unitsManager, attacker);
			HostileUnitSearchResult nearestHostileBuilding = SearchForNearest(_buildingsManager, attacker);

			if (nearestHostileUnit.Distance <= nearestHostileBuilding.Distance)
				return nearestHostileUnit.GameUnit;
			else
				return nearestHostileBuilding.GameUnit;
		}


		private struct HostileUnitSearchResult
		{
			public GameActor GameUnit { get; set; }
			public float Distance { get; set; }
		}

		private static HostileUnitSearchResult SearchForNearest<T, TS>(GameActorsManager<T, TS> unitsManager, GameUnit attacker)
			where TS : GameActor
		{
			HostileUnitSearchResult result = new HostileUnitSearchResult
			{
				GameUnit = null,
				Distance = float.MaxValue
			};

			foreach (var currentGameObject in unitsManager.GetRegistered().Where(
				u => u.ActorAttributes.Team != attacker.ActorAttributes.Team && u.ActorAttributes.HealthPoints > 0f)
			)
			{
				float currentDistance = Vector3.Distance(
					attacker.Transform.localPosition,
					currentGameObject.Transform.localPosition
				);

				if (currentDistance > result.Distance)
					continue;

				result.Distance = currentDistance;
				result.GameUnit = currentGameObject;
			}

			return result;
		}

		private TargetSelector()
		{
			_unitsManager = UnitsManager.GetInstance();
			_buildingsManager = BuildingsManager.GetInstance();
		}

		private static TargetSelector _targetSelector;

		private readonly UnitsManager _unitsManager;
		private readonly BuildingsManager _buildingsManager;
	}
}