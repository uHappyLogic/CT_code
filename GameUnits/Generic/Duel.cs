using Assets.Scripts.Gui;
using Assets.Scripts.Multi;
using Mirror;

namespace Assets.Scripts.GameUnits.Generic
{
	public class Duel : NetworkBehaviour
	{
		public GameUnit Attacker { get; set; }
		public GameActor Defender { get; set; }

		public void ApplyResult()
		{
			// apply result is triggered by animation
			// so every animation within instances can trigger that
			if (!hasAuthority)
				return;

			if (Defender == null)
				return;

			if (Defender.LifeState != GameActor.UnitLifeState.LIVING)
				return;
			
			CmdPerfomAttack(Defender.UniqeNetworkId, Attacker.ActorAttributes.Team, Attacker.UnitAttributes.AttackPoints);
		}

		[Command]
		public void CmdPerfomAttack(string defenderId, Team attackerTeam, float attackPoints)
		{
			GameActor defender = UnitsManager.GetInstance().GetByUniqueNetworkId(defenderId);

			if (defender == null)
				defender = BuildingsManager.GetInstance().GetByUniqueNetworkId(defenderId);

			VisibleLogger.GetInstance().LogDebug(
				string.Format("Damage [{0}] for {1}",
					defender.GetId()
					, attackPoints
			));

			defender.ActorAttributes.HealthPoints -= attackPoints;

			if (HpLessThanZero(defender) && defender.LifeState == GameActor.UnitLifeState.LIVING)
			{
				VisibleLogger.GetInstance().LogDebug(
					string.Format("Changing [{0}] state {1} -> {2}",
						defender.GetId()
						, defender.LifeState,
						GameActor.UnitLifeState.WAITING_FOR_DEAD_ACTION
				));

				PlayersManager.GetInstance().Get(attackerTeam).Gold += defender.ActorAttributes.KillReward;

				defender.LifeState = GameActor.UnitLifeState.WAITING_FOR_DEAD_ACTION;
			}
		}

		[Command]
		public void CmdSetWaitingForDeadActionState()
		{
			Defender.LifeState = GameActor.UnitLifeState.WAITING_FOR_DEAD_ACTION;
		}

		private static bool HpLessThanZero(GameActor ga)
		{
			return ga.ActorAttributes.HealthPoints <= 0f;
		}
	}
}