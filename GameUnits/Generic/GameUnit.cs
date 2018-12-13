using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.GameUnits.Generic
{
	public abstract class GameUnit : GameActor
	{
		public Animator Animator { get; private set; }
		public NavMeshAgent NavMeshAgent { get; private set; }
		public Duel Duel { get; private set; }
		public UnitAttributes UnitAttributes { get; set; }

		public override void Init()
		{
			base.Init();

			UnitAttributes = GetComponent<UnitAttributes>();
			Animator = GetComponent<Animator>();
			NavMeshAgent = GetComponent<NavMeshAgent>();
			Duel = GetComponent<Duel>();
			Duel.Attacker = this;
		}
	}
}
