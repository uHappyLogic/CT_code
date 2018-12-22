using System;
using System.Linq;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Core
{
	public abstract class MonoBehaviourSingletonCapabilityGameActorManager<TS, T> 
        : MonoBehaviourSingletonCapabilityManager<TS, T> 
        where T: GameActor
    {
        public void Update()
        {
			if (!isServer)
				return;

            foreach (var unit in _registered.Where(u => u.CanBeUnregistered()))
            {
                Debug.Log(String.Format("Destroying unit {0}", unit.GetId()));
				NetworkServer.Destroy(unit.GameObject);
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
