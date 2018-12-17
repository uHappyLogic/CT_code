using System;
using System.Timers;
using UnityEngine;

namespace Assets.Scripts.GameUnits.Generic
{
	public class GameUnitFactory : GameBuilding
	{
		public GameObject PrefabToSpawn;

		public override void Init()
		{
			Debug.Log("Factory initialization");
			base.Init();

			_productionOutput = Transform.localPosition + Transform.localRotation * new Vector3(8, 0, 0);

			_isConstructionComplete = false;

			_constructionStartTime = Time.time;

			Debug.Log(String.Format("Factory [{0}] initialization finished successfully", GetId()));
		}

		private void Stop()
		{
			_timer.Stop();
			_timer.Close();
		}

		public override void UpdateWhenUnderConstruction()
		{
			Transform.localPosition += Vector3.up * Time.deltaTime * 1f;

			if (Time.time - _constructionStartTime >= BuildingAttributes.ConstructionTime)
				_isConstructionComplete = true;
		}

		public override bool IsConstructionComplete()
		{
			return _isConstructionComplete;
		}

		public override void OnConstructionComplete()
		{
			Debug.Log(String.Format("Construction completed for {0}", GetId()));
			_timer = new Timer(TimeSpan.FromSeconds(BuildingAttributes.MainActionCooldown).TotalMilliseconds);
			_timer.AutoReset = true;
			_timer.Elapsed += SetSpawnOn;
			_timer.Start();
		}

		public override void CompleteConstruction()
		{
			_isConstructionComplete = true;
		}

		public override void OnDeadAction()
		{
			_timeOfDie = Time.time;
			_shouldSpawn = false;
			Stop();
		}

		public override bool CanBeUnregistered()
		{
			return _canBeUnregistered;
		}

		public override void UpdateAliveGameUnit()
		{
			if (_shouldSpawn)
				Spawn();
		}

		public override void UpdateDeadGameUnit()
		{
			Transform.localPosition += Vector3.down * Time.deltaTime * 3f;

			if (Time.time - _timeOfDie > 8f)
				_canBeUnregistered = true;
		}

		private void SetSpawnOn(object sender, ElapsedEventArgs elapsedEventArgs)
		{
			_shouldSpawn = true;
		}

		private void Spawn()
		{
			_shouldSpawn = false;

			var instantiatedPrefab = Instantiate<GameObject>(PrefabToSpawn, _productionOutput, Quaternion.identity);
			GameUnit gameUnit = instantiatedPrefab.GetComponent<GameUnit>();

			gameUnit.Init();
			gameUnit.ActorAttributes.Team = BuildingAttributes.Team;
			gameUnit.gameObject.name = gameUnit.GetId();

			UnitsManager.GetInstance().Add(gameUnit);
		}

		private bool _shouldSpawn;
		private Timer _timer;
		private Vector3 _productionOutput = new Vector3(-50f, 0f, 15f);

		private bool _canBeUnregistered;
		private float _timeOfDie;
		private readonly Transform _transform;
		private readonly ActorAttributes _actorAttributes;
		private readonly GameObject _gameObject;
		private bool _isConstructionComplete;
		private float _constructionStartTime;

	}
}
