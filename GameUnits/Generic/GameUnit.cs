using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.GameUnits.Generic
{
	public abstract class GameUnit : GameActor
	{
		[SerializeField]
		public Animator Animator;

		[SerializeField]
		public NavMeshAgent NavMeshAgent;

		[SerializeField]
		public Duel Duel;

		[SerializeField]
		public UnitAttributes UnitAttributes;

		public void Start()
		{
			Duel.Attacker = this;
		}
	}
}
