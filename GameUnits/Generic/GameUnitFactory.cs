using Assets.Scripts.Multi;
using UnityEngine;
using UnityEngine.AI;
using Mirror;
using Assets.Scripts.Gui;

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
				_isConstructionComplete = true;
		}

		public override bool IsConstructionComplete()
		{
			return _isConstructionComplete;
		}

		public override void OnConstructionComplete()
		{
			_timeOfLastSpawn = Time.time;
			VisibleLogger.GetInstance().LogDebug(
				string.Format("OnConstructionComplete [{0}]", GetId())
			);
		}

		public override void CompleteConstruction()
		{
			_isConstructionComplete = true;
		}

		public override void OnDeadAction()
		{
			VisibleLogger.GetInstance().LogDebug(
				string.Format("OnDeadAction [{0}]", GetId())
			);

			_timeOfDie = Time.time;

			Destroy(GetComponent<NavMeshObstacle>());
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
						, (1 - ((totalDyingTime) / DYING_TIME)) * -10
						, 0
					)
			);

			if (totalDyingTime > DYING_TIME)
				CanBeUnregistered = true;
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
