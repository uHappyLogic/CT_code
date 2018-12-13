using System.Linq;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;

namespace Assets.Scripts.GameUnits
{
    public class TargetSelector
    {
        public static TargetSelector GetInstance()
        {
            if (_targetSelector == null)
            {
                Debug.Log("Target selector initialized");
                _targetSelector = new TargetSelector();
            }

            return _targetSelector;
        }

        public GameUnit GetNearestHostileUnit(GameUnit attacker)
        {
            GameUnit nearestHostile = null;
            float minDistance = float.MaxValue;

            foreach (var currentGameObject in _unitsManager.GetRegistered().Where(
                u => u.ActorAttributes.Team != attacker.ActorAttributes.Team && u.ActorAttributes.HealthPoints > 0f)
            )
            {
                float currentDistance = Vector3.Distance(
                    attacker.Transform.localPosition,
                    currentGameObject.Transform.localPosition
                );

                if (currentDistance > minDistance)
                    continue;

                minDistance = currentDistance;
                nearestHostile = currentGameObject;
            }

            return nearestHostile;
        }

        private TargetSelector()
        {
            _unitsManager = UnitsManager.GetInstance();
            _buildingsManager = BuildingsManager.GetInstance();
        }

        private static TargetSelector _targetSelector;

        private UnitsManager _unitsManager;
        private BuildingsManager _buildingsManager;
    }
}