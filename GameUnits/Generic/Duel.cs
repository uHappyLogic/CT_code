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

            bool defenderAlive = Defender.ActorAttributes.HealthPoints > 0f;

            Defender.ActorAttributes.HealthPoints -= Attacker.UnitAttributes.AttackPoints;

            if (defenderAlive &&  Defender.ActorAttributes.HealthPoints <= 0)
            {
                KillsCounter.GetInstance().Increment(Defender.ActorAttributes.Team);
                Defender.OnDeadAction();
            }
        }
    }
}