﻿using UnityEngine;

namespace Assets.Scripts.GameUnits.Generic
{
	public abstract class GameBuilding : GameActor
	{
		[SerializeField]
		public BuildingAttributes BuildingAttributes;

		public void Start()
		{
			BuildingsManager.GetInstance().Add(this);
		}

		public abstract void UpdateWhenUnderConstruction();

		public abstract bool IsConstructionComplete();

		public abstract void OnConstructionComplete();

		public abstract void CompleteConstruction();
	}
}
