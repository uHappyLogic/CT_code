using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;

namespace Assets.Scripts.GameUnits
{
	public class BuildingsManager : SingletonCapabilityManager<BuildingsManager, GameBuilding>
	{
		protected override void CustomInitialization()
		{

		}

		public override void UpdateLifecycle()
		{
			foreach (var registeredGameUnit in _registered)
			{
				if (!registeredGameUnit.IsConstructionComplete())
				{
					registeredGameUnit.UpdateWhenUnderConstruction();
					return;
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
	}
}