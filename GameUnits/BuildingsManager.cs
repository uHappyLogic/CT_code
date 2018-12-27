using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;
using Assets.Scripts.Gui;

namespace Assets.Scripts.GameUnits
{
	public class BuildingsManager : GameActorsManager<BuildingsManager, GameBuilding>
	{
		public static BuildingsManager GetInstance()
		{
			if (_buildingsManager == null)
				_buildingsManager = new BuildingsManager();

			return _buildingsManager;
		}

		public override void UpdateLifecycle()
		{
			foreach (var registeredGameUnit in _registered)
			{
				if (!registeredGameUnit.hasAuthority)
					continue;

				switch (registeredGameUnit.LifeState)
				{
					case GameActor.UnitLifeState.UNDER_CONSTRUCTION:
						registeredGameUnit.UpdateWhenUnderConstruction();
						break;

					case GameActor.UnitLifeState.WAITING_FOR_ON_CONSTRUCTED:
						registeredGameUnit.OnConstructionComplete();
						break;

					case GameActor.UnitLifeState.LIVING:
						registeredGameUnit.UpdateAliveGameUnit();
						break;

					case GameActor.UnitLifeState.WAITING_FOR_DEAD_ACTION:
						registeredGameUnit.OnDeadAction();
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
			return "BuildingsManager";
		}

		protected override void LogAdded(GameBuilding actor)
		{
			VisibleLogger.GetInstance().LogDebug(string.Format(
				"Building [{0}] registered",
				actor.GetId()
			));
		}

		private static BuildingsManager _buildingsManager;
	}
}