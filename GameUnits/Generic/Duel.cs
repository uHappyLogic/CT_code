using Assets.Scripts.Gui;
using UnityEngine;

namespace Assets.Scripts.GameUnits.Generic
{
	public class Duel : MonoBehaviour
	{
		public GameUnit Attacker { get; set; }
		public GameActor Defender { get; set; }

		public void ApplyResult()
		{
			if (Defender == null)
				return;

			bool wasDefenderAliveBeforeAttack = IsAlive(Defender);

			Defender.ActorAttributes.HealthPoints -= Attacker.UnitAttributes.AttackPoints;

			if (wasDefenderAliveBeforeAttack && !IsAlive(Defender))
			{
				KillsCounter.GetInstance().Increment(Defender.ActorAttributes.Team);
				Defender.OnDeadAction();
			}
		}

		private static bool IsAlive(GameActor ga)
		{
			return ga.ActorAttributes.HealthPoints > 0f;
		}
	}
}