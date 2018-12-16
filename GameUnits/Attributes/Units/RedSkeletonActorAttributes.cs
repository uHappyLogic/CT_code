using Assets.Scripts.GameUnits.Generic;

namespace Assets.Scripts.GameUnits.Attributes.Units
{
    public class RedSkeletonActorAttributes : UnitAttributes
    {
        public override void InitActorAttributes(Team owner)
        {
            AttackPoints = 50f;
            Team = owner;
            HealthPoints = 2000f;
            ActorTypeName = "SkeletonRed";
        }
    }
}