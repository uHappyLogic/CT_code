using Mirror;
using UnityEngine;

namespace Assets.Scripts.GameUnits.Generic
{
	public abstract class GameActor : NetworkBehaviour
	{
		[SerializeField]
		public ActorAttributes ActorAttributes;

		[SerializeField]
		public Transform Transform;

		[SerializeField]
		[Tooltip("Currently this object is only available for buildings")]
		public Collider Collider;

		public string GetId()
		{
			return string.Format("{0}_{1}_{2}"
				, ActorAttributes.ActorTypeName
				, UniqeNetworkId
				, ActorAttributes.Team.ToString());
		}

		/*
         * Method is called once by Duel in which Health reached less than zero
         */
		public abstract void OnDeadAction();


		public enum UnitLifeState
		{
			UNDER_CONSTRUCTION,
			WAITING_FOR_ON_CONSTRUCTED,
			LIVING,
			WAITING_FOR_DEAD_ACTION,
			DEAD,
			WAITING_FOR_DISPOSAL
		}

		/*
		 * True if unit is dead
		 */
		[SyncVar]
		public UnitLifeState LifeState;


		[SyncVar]
		public string UniqeNetworkId;

		/*
         * Method is called by UnitsManager once per few frames when units Health is greater than zero
         */
		public abstract void UpdateAliveGameUnit();

		/*
         * Method is called by UnitsManager once per few frames when units Health is less than zero
         */
		public abstract void UpdateDeadGameUnit();
	}
}
