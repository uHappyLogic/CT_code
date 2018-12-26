using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;
using Assets.Scripts.Gui;

namespace Assets.Scripts.GameUnits
{
	public class UnitsManager : GameActorsManager<UnitsManager, GameUnit>
	{
		public static UnitsManager GetInstance()
		{
			if (_unitsManager == null)
				_unitsManager = new UnitsManager();

			return _unitsManager;
		}

		public override void UpdateLifecycle()
		{
			foreach (var registeredGameUnit in _registered)
			{
				if (!registeredGameUnit.hasAuthority)
					continue;

				switch(registeredGameUnit.LifeState)
				{
					case GameActor.UnitLifeState.WAITING_FOR_DEAD_ACTION:
						registeredGameUnit.OnDeadAction();
						break;

					case GameActor.UnitLifeState.LIVING:
						registeredGameUnit.UpdateAliveGameUnit();
						break;

					case GameActor.UnitLifeState.DEAD:
						registeredGameUnit.UpdateDeadGameUnit();
						break;

					default:
						break;

				}
			}
		}

		public override string GetName()
		{
			return "UnitsManager";
		}

		protected override void LogAdded(GameUnit actor)
		{
			VisibleLogger.GetInstance().LogDebug(string.Format(
				"Unit [{0}] registered",
				actor.GetId()
			));
		}

		private static UnitsManager _unitsManager;
	}
}