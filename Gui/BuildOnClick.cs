using Assets.Scripts;
using Assets.Scripts.GameUnits;
using Assets.Scripts.GameUnits.Generic;
using UnityEngine;

public class BuildOnClick : MonoBehaviour {

    public GameObject BuildingToBuild;
    public TerrainPointerController TerrainPointer;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        var newFactory = Instantiate(BuildingToBuild);

        newFactory.transform.position = TerrainPointer.transform.position + Vector3.up * 3f;

        GameBuilding gameBuilding = newFactory.GetComponent<GameBuilding>();
        gameBuilding.Init();
        gameBuilding.ActorAttributes.InitActorAttributes(Team.TEAM_A);
        gameBuilding.CompleteConstruction();
        gameBuilding.OnConstructionComplete();

        BuildingsManager.GetInstance().Add(gameBuilding);

        TerrainPointer.GetComponent<TerrainPointerController>().DetachObject();
    }
}
