namespace Assets.Scripts.GameUnits.Generic
{
	public abstract class GameBuilding : GameActor
	{
		public BuildingAttributes BuildingAttributes { get; private set; }

		public override void Init()
		{
			base.Init();

			BuildingAttributes = GetComponent<BuildingAttributes>();
		}

		public abstract void UpdateWhenUnderConstruction();

		public abstract bool IsConstructionComplete();

		public abstract void OnConstructionComplete();

		public abstract void CompleteConstruction();
	}
}
