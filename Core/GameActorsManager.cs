using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameUnits.Generic;
using Assets.Scripts.Gui;
using Mirror;

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
				try
				{
					_uniqueNetworkIdToGameActorDictionary.Remove(unit.UniqeNetworkId);

					if (unit.hasAuthority)
						NetworkServer.Destroy(unit.gameObject);

					VisibleLogger.GetInstance().LogDebug(
						string.Format("<color=orange>Destroyed [{0}]</color>", unit.GetId())
					);
				}
				catch (Exception ex)
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

		private void RemoveNetworkid(string networkId)
		{
			if (!_uniqueNetworkIdToGameActorDictionary.Remove(networkId))
			{
				VisibleLogger.GetInstance().LogError(
					string.Format("Missing {0}", networkId)
				);
			}
		}

		public abstract string GetName();

		protected abstract void LogAdded(T actor);

		protected List<T> _registered = new List<T>();

		protected Dictionary<string, T> _uniqueNetworkIdToGameActorDictionary = new Dictionary<string, T>();
	}
}
