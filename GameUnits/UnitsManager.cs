using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;

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
				if (registeredGameUnit.ActorAttributes.HealthPoints > 0)
					registeredGameUnit.UpdateAliveGameUnit();
				else
					registeredGameUnit.UpdateDeadGameUnit();
			}
		}

		public override string GetName()
		{
			return "UnitsManager";
		}

		private static UnitsManager _unitsManager;
	}
}