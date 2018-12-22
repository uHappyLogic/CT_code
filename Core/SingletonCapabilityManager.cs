using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
	public abstract class SingletonCapabilityManager<TS, T>
		: SingletonCapability<TS>, IUpdateable
		where T : GameActor
	{
		protected override void CoreInitialize()
		{
			base.CoreInitialize();

			_registered = new List<T>();
		}

		public void PerformUpdate()
		{
			foreach (var unit in _registered.Where(u => u.CanBeUnregistered()))
			{
				Debug.Log(String.Format("<color=orange>Destroying</color> unit {0}", unit.GetId()));
				UnityEngine.Object.Destroy(unit.GameObject);
			}

			_registered.RemoveAll(u => u.CanBeUnregistered());

			UpdateLifecycle();
		}

		public abstract void UpdateLifecycle();

		public void Add(T actor)
		{
			Debug.Log(String.Format(
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

		protected List<T> _registered;
	}
}
