using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFactory : BuildActive
{

    public BuildingFactory(StateMachineHandler stateMachineHandler) : base(stateMachineHandler){}



    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _buildingController._factoryHologram.gameObject.SetActive(true);
    }



    public override void Execute()
    {
        base.Execute();
        _buildingController.FollowHoloBuilding(_buildingController._factoryHologram.gameObject);

        if(Input.GetKeyDown(Controls.keys._confirm)) {
            if(_buildingController.BuildFloorCheck() && _buildingController.BuildOverlapCheck(_buildingController._factoryHologram)) { //check if the space we are building is valid
                _buildingController.InstantiateMachine(_buildingController._factoryGO);
            }
        }
    }



    public override void OnStateLeave()
    {
        base.OnStateLeave();
        _buildingController._factoryHologram.gameObject.SetActive(false);
    }
}
