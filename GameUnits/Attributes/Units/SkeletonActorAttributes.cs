using Assets.Scripts.GameUnits.Generic;

namespace Assets.Scripts.GameUnits.Attributes.Units
{
    public class SkeletonActorAttributes : UnitAttributes
    {
        public override void InitActorAttributes(Team owner)
        {
            AttackPoints = 20f;
            Team = owner;
            HealthPoints = 100f;
            ActorTypeName = "Skeleton";
        }
    }
}