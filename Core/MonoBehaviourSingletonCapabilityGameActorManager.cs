using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class MonoBehaviourSingletonCapabilityGameActorManager<TS, T> 
        : MonoBehaviourSingletonCapabilityManager<TS, T> 
        where T: GameActor
    {
        public void Update()
        {
            foreach (var unit in _registered.Where(u => u.CanBeUnregistered()))
            {
                Debug.Log(String.Format("Destroying unit {0}", unit.GetId()));
                Destroy(unit.GameObject);
            }

            _registered.RemoveAll(u => u.CanBeUnregistered());

            UpdateLifecycle();
        }

        abstract public void UpdateLifecycle();

        public override void LogAddOperation(T actor)
        {
            Debug.Log(String.Format(
                "Unit with id {0} registered to {1}",
                actor.GetId(),
                GetName()
            ));
        }
    }
}
