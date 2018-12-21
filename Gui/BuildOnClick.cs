using Assets.Scripts;
using Assets.Scripts.Core;
using Assets.Scripts.GameUnits;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuildOnClick : NetworkBehaviour
{
	public GameObject BuildingToBuild;

	// Use this for initialization
	private void Start()
	{

	}

	// Update is called once per frame
	private void Update()
	{

	}

	private void OnMouseDown()
	{
		// TODO: check player authority
		CmdSpawnBuilding();
		TerrainPointerControllerProvider.GetInstance().DetachObject();
	}

	[Command]
	private void CmdSpawnBuilding()
	{
		var newFactory = Instantiate(BuildingToBuild);

		newFactory.transform.position = TerrainPointerControllerProvider.GetInstance().transform.position + Vector3.up * 3f;

		GameBuilding gameBuilding = newFactory.GetComponent<GameBuilding>();
		gameBuilding.Init();
		gameBuilding.ActorAttributes.Team = Team.TEAM_A;
		gameBuilding.CompleteConstruction();
		gameBuilding.OnConstructionComplete();

		NetworkServer.Spawn(newFactory);

		BuildingsManager.GetInstance().Add(gameBuilding);
	}
}
