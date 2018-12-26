using Assets.Scripts.Gui;
using Mirror;

namespace Assets.Scripts.GameUnits.Generic
{
	public class Duel : NetworkBehaviour
	{
		public GameUnit Attacker { get; set; }
		public GameActor Defender { get; set; }

		public void ApplyResult()
		{
			if (Defender == null)
				return;

			bool wasDefenderAliveBeforeAttack = IsAlive(Defender);

			CmdPerfomAttack();

			if (wasDefenderAliveBeforeAttack && !IsAlive(Defender))
			{
				KillsCounter.GetInstance().Increment(Defender.ActorAttributes.Team);
				Defender.OnDeadAction();
			}
		}

		[Command]
		public void CmdPerfomAttack()
		{
			Defender.ActorAttributes.HealthPoints -= Attacker.UnitAttributes.AttackPoints;
		}

		private static bool IsAlive(GameActor ga)
		{
			return ga.ActorAttributes.HealthPoints > 0f;
		}
	}
}