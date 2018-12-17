using System;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameUnits.Generic
{
    public abstract class ActorAttributes : MonoBehaviour
    {
		[SerializeField]
		public Team Team;

		[SerializeField]
		public float HealthPoints;

		[SerializeField]
		public string ActorTypeName;
    }
}