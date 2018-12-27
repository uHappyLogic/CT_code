using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameUnits.Generic;
using Mirror;
using Assets.Scripts.Gui;
using UnityEngine;

namespace Assets.Scripts.Core
{
	public abstract class GameActorsManager<TS, T>
		: IUpdateable
		where T : GameActor
	{
		public void PerformUpdate()
		{
			foreach (var unit in _registered.Where(
				u => u.LifeState == GameActor.UnitLifeState.WAITING_FOR_DISPOSAL
			))
			{
				VisibleLogger.GetInstance().LogDebug(
					string.Format("<color=orange>Destroying</color> [{0}]", unit.GetId())
				);

				_uniqueNetworkIdToGameActorDictionary.Remove(unit.UniqeNetworkId);
				try
				{
					NetworkServer.Destroy(unit.gameObject);
				}
				catch (MissingReferenceException ex)
				{
					VisibleLogger.GetInstance().LogException(ex);
				}
			}

			int removedCount = _registered.RemoveAll(
				u => u.LifeState == GameActor.UnitLifeState.WAITING_FOR_DISPOSAL
			);

			if (removedCount > 0)
			{
				VisibleLogger.GetInstance().LogDebug(
					string.Format("Removed {0} objects", removedCount)
				);
			}

			UpdateLifecycle();
		}

		public abstract void UpdateLifecycle();

		public void Add(T actor)
		{
			LogAdded(actor);
			_registered.Add(actor);
			_uniqueNetworkIdToGameActorDictionary.Add(actor.UniqeNetworkId, actor);
		}

		public T GetByUniqueNetworkId(string uniqueNetworkId)
		{
			T res;
			_uniqueNetworkIdToGameActorDictionary.TryGetValue(uniqueNetworkId, out res);

			return res;
		}

		public List<T> GetRegistered()
		{
			return _registered;
		}

		public abstract string GetName();

		protected abstract void LogAdded(T actor);

		protected List<T> _registered = new List<T>();

		protected Dictionary<string, T> _uniqueNetworkIdToGameActorDictionary = new Dictionary<string, T>();
	}
}
