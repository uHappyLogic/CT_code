using Assets.Scripts.GameUnits.Generic;

namespace Assets.Scripts.GameUnits.Attributes.Buildings
{
	public class GraveyardRedBuildingAttributes : BuildingAttributes
	{
		public override void InitActorAttributes(Team owner)
		{
			ActorTypeName = "Graveyard RED";
			Team = owner;
			HealthPoints = 1000f;
			BuildCost = 100;
			ConstructionTime = 7.5f;
			MainActionCooldown = 5f;
		}
	}
}