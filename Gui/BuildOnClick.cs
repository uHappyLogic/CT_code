using Assets.Scripts;
using Assets.Scripts.Core;
using Assets.Scripts.GameUnits.Generic;
using Assets.Scripts.Multi;
using UnityEngine;
using Mirror;

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
		CmdSpawnBuilding(
			TerrainPointerControllerProvider.GetInstance().transform.position + Vector3.up * 3f
			, PlayerScript.GetInstance().Team
		);

		TerrainPointerControllerProvider.GetInstance().DetachObject();
	}

	[Command]
	private void CmdSpawnBuilding(Vector3 position, Team team)
	{
		var newFactory = Instantiate(BuildingToBuild);

		newFactory.transform.position = position;

		GameBuilding gameBuilding = newFactory.GetComponent<GameBuilding>();
		gameBuilding.ActorAttributes.Team = team;
		gameBuilding.UniqeNetworkId = gameBuilding.GetHashCode().ToString();

		NetworkServer.SpawnWithClientAuthority(
			newFactory,
			PlayersManager.GetInstance().Get(team).connectionToClient
		);
	}
}
