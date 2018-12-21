using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.GameUnits.Generic
{
	public abstract class GameActor : NetworkBehaviour
	{
		public ActorAttributes ActorAttributes { get; private set; }
		public Transform Transform { get; private set; }
		public GameObject GameObject { get; set; }
		public Collider Collider { get; set; }

		public virtual void Init()
		{
			ActorAttributes = GetComponent<ActorAttributes>();
			Transform = GetComponent<Transform>();
			GameObject = gameObject;
			Collider = GetComponent<Collider>();
		}

		public String GetId()
		{
			return String.Format("{0}_{1}", ActorAttributes.ActorTypeName, GetHashCode());
		}

		/*
         * Method is called once by Duel in which Health reached less than zero
         */
		public abstract void OnDeadAction();

		/*
         * If true then will be unregistered in next iteration, otherwise unit will remain registered
         */
		public abstract bool CanBeUnregistered();

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
