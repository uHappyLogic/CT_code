using System;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.GameUnits.Generic
{
    public abstract class ActorAttributes : MonoBehaviour
    {
        public Team Team { get; set; }
        public float HealthPoints { get; set; }
        public string ActorTypeName { get; set; }

        public abstract void InitActorAttributes(Team owner);
    }
}