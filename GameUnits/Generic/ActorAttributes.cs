using UnityEngine;
using Mirror;

namespace Assets.Scripts.GameUnits.Generic
{
	public abstract class ActorAttributes : NetworkBehaviour
    {
		[SerializeField]
		[SyncVar]
		public Team Team;

		[SerializeField]
		[SyncVar]
		public float HealthPoints;

		[SerializeField]
		public string ActorTypeName;

		[SerializeField]
		public int KillReward;
    }
}