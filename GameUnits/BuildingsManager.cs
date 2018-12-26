using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;

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

				if (!registeredGameUnit.IsConstructionComplete())
				{
					registeredGameUnit.UpdateWhenUnderConstruction();
					continue;
				}

				if (registeredGameUnit.ActorAttributes.HealthPoints > 0)
					registeredGameUnit.UpdateAliveGameUnit();
				else
					registeredGameUnit.UpdateDeadGameUnit();
			}
		}

		public override string GetName()
		{
			return "BuildingsManager";
		}

		private static BuildingsManager _buildingsManager;
	}
}