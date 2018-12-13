using Assets.Scripts.GameUnits.Generic;

namespace Assets.Scripts.GameUnits.Attributes.Buildings
{
	public class GraveyardBuildingAttributes : BuildingAttributes
	{
		public override void InitActorAttributes(Team owner)
		{
			ActorTypeName = "Graveyard";
			Team = owner;
			HealthPoints = 1000f;
			BuildCost = 100;
			ConstructionTime = 5f;
			MainActionCooldown = 5f;
		}
	}
}