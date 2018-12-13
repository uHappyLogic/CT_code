namespace Assets.Scripts.GameUnits.Generic
{
    public abstract class BuildingAttributes : ActorAttributes
    {
        public float MainActionCooldown { get; set; }
        public float ConstructionTime { get; set; }
        public int BuildCost { get; set; }

    }
}