using UnityEngine;

namespace Assets.Scripts.GameUnits.Generic
{
    public class BuildingAttributes : ActorAttributes
    {
		[SerializeField]
		public float MainActionCooldown;

		[SerializeField]
		public float ConstructionTime;

		[SerializeField]
		public int BuildCost;
    }
}