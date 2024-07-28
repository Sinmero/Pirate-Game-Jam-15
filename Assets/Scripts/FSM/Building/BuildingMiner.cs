using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMiner : BuildActive
{
    public BuildingMiner(StateMachineHandler stateMachineHandler) : base(stateMachineHandler)
    {
        _buildingController = _stateMachineHandler as BuildingController;
    }



    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _buildingController._minerHologram.gameObject.SetActive(true);
    }



    public override void Execute()
    {
        base.Execute();
        _buildingController.FollowHoloBuilding(_buildingController._minerHologram.gameObject);

        if(Input.GetKeyDown(Controls.keys._confirm)) {
            if(_buildingController._minerHologram._resourceVein == null) return; // if there is no resource vein nearby
            if(_buildingController.BuildFloorCheck() && _buildingController.BuildOverlapCheck(_buildingController._minerHologram)) { //check if the space we are building on is valid
                Vector3 pos = _buildingController._minerHologram._resourceVein.transform.position;
                pos.y += 1;
                var newMachine = _buildingController.InstantiateMachine(_buildingController._minerGO, pos); //spawn mining machine at ore location
                MiningMachine miningMachine = newMachine.GetComponent<MiningMachine>();
                miningMachine._resource = _buildingController._minerHologram._resourceVein._resourceName; //set the name of the vein resource to the mining machine
                miningMachine._resourceState = Items.instance._itemDictionary[miningMachine._resource]._defaultState; //set the default state for the resourse type
                miningMachine._resourceVein = _buildingController._minerHologram._resourceVein;
            }
        }
    }



    public override void OnStateLeave()
    {
        base.OnStateLeave();
        _buildingController._minerHologram.gameObject.SetActive(false);
    }
}
