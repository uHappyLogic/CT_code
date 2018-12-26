using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;
using Mirror;
using Assets.Scripts.Gui;

namespace Assets.Scripts.Core
{
	public abstract class GameActorsManager<TS, T>
		: IUpdateable
		where T : GameActor
	{
		public void PerformUpdate()
		{
			foreach (var unit in _registered.Where(u => u.CanBeUnregistered()))
			{
				VisibleLogger.GetInstance().LogDebug(string.Format("<color=orange>Destroying</color> unit {0}", unit.GetId()));
				NetworkServer.Destroy(unit.gameObject);
			}

			_registered.RemoveAll(u => u.CanBeUnregistered());

			UpdateLifecycle();
		}

		public abstract void UpdateLifecycle();

		public void Add(T actor)
		{
			VisibleLogger.GetInstance().LogDebug(string.Format(
				"Unit with id {0} registered to {1}",
				actor.GetId(),
				GetName()
			));

			_registered.Add(actor);
		}

		public List<T> GetRegistered()
		{
			return _registered;
		}

		public abstract string GetName();

		protected List<T> _registered = new List<T>();
	}
}
