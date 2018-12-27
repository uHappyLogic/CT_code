using Assets.Scripts.Gui;
using Assets.Scripts.Multi;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.GameUnits.Generic
{
	public class GameUnitFactory : GameBuilding
	{
		[SerializeField]
		[Tooltip("This prefab will be spawned by this building")]
		public GameObject PrefabToSpawn;

		public new void Start()
		{
			base.Start();

			_productionOutput = Transform.localPosition + Transform.localRotation * new Vector3(8, 0, 0);
			_targetPositionAfterBuild = Transform.localPosition;

			_isConstructionComplete = false;

			_constructionStartTime = Time.time;

			VisibleLogger.GetInstance().LogDebug(
				string.Format("Start [{0}]", GetId())
			);
		}

		public override void UpdateWhenUnderConstruction()
		{
			float totalBuildingTime = Time.time - _constructionStartTime;

			Transform.localPosition = (
				_targetPositionAfterBuild + new Vector3(
					0
					, (1 - ((totalBuildingTime) / BuildingAttributes.ConstructionTime)) * -10
					, 0
				)
			);

			if (totalBuildingTime >= BuildingAttributes.ConstructionTime)
			{
				VisibleLogger.GetInstance().LogDebug(
					string.Format("Constucted [{0}]", GetId())
				);

				CmdChangeStateToWaitingForOnConstructed();
			}
		}

		[Command]
		private void CmdChangeStateToWaitingForOnConstructed()
		{
			LifeState = UnitLifeState.WAITING_FOR_ON_CONSTRUCTED;
		}

		public override void OnConstructionComplete()
		{
			_timeOfLastSpawn = Time.time;

			VisibleLogger.GetInstance().LogDebug(
				string.Format("OnConstructionComplete [{0}]", GetId())
			);

			// Events that shall occur only one time per object lifetime
			// have to be set locally to prevent multiple invocation
			LifeState = UnitLifeState.LIVING;
			CmdChangeStateToLiving();
		}

		[Command]
		private void CmdChangeStateToLiving()
		{
			LifeState = UnitLifeState.LIVING;
		}

		public override void OnDeadAction()
		{
			VisibleLogger.GetInstance().LogDebug(
				string.Format("OnDeadAction [{0}]", GetId())
			);

			_timeOfDie = Time.time;

			LifeState = UnitLifeState.DEAD;
			CmdChangeLifeStateToDead();
		}

		[Command]
		private void CmdChangeLifeStateToDead()
		{
			LifeState = UnitLifeState.DEAD;
		}

		public override void UpdateAliveGameUnit()
		{
			float totalSpawningTime = Time.time - _timeOfLastSpawn;

			if (totalSpawningTime >= BuildingAttributes.MainActionCooldown)
				Spawn();
		}

		public override void UpdateDeadGameUnit()
		{
			float totalDyingTime = Time.time - _timeOfDie;

			Transform.localPosition = (
				_targetPositionAfterBuild + new Vector3(
					0
					, ((totalDyingTime) / BuildingAttributes.ConstructionTime) * -10
					, 0
				)
			);

			if (totalDyingTime > DYING_TIME)
				CmdChangeLifeStateToWaitingForDisposal();
		}

		[Command]
		private void CmdChangeLifeStateToWaitingForDisposal()
		{
			LifeState = UnitLifeState.WAITING_FOR_DISPOSAL;
		}

		private void Spawn()
		{
			_timeOfLastSpawn = Time.time;
			CmdSpwanUnit();
		}

		[Command]
		private void CmdSpwanUnit()
		{
			var instantiatedPrefab = Instantiate(PrefabToSpawn, _productionOutput, Quaternion.identity);
			GameUnit gameUnit = instantiatedPrefab.GetComponent<GameUnit>();

			gameUnit.ActorAttributes.Team = BuildingAttributes.Team;
			gameUnit.UniqeNetworkId = gameUnit.GetHashCode().ToString();

			NetworkServer.SpawnWithClientAuthority(
				instantiatedPrefab,
				PlayersManager.GetInstance().Get(BuildingAttributes.Team).connectionToClient
			);
		}

		private Vector3 _productionOutput = new Vector3(-50f, 0f, 15f);
		private Vector3 _targetPositionAfterBuild;

		private float _timeOfLastSpawn;
		private float _timeOfDie;
		private const float DYING_TIME = 8f;

		private bool _isConstructionComplete;
		private float _constructionStartTime;
	}
}
