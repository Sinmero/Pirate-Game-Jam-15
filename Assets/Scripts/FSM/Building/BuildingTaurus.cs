using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTaurus : BuildActive
{
    public BuildingTaurus(StateMachineHandler stateMachineHandler) : base(stateMachineHandler){}



    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _buildingController._taurusHologram.gameObject.SetActive(true);
    }



    public override void Execute()
    {
        base.Execute();
        _buildingController.FollowHoloBuilding(_buildingController._taurusHologram.gameObject);

        if(Input.GetKeyDown(Controls.keys._confirm)) {
            if(_buildingController.BuildFloorCheck() && _buildingController.BuildOverlapCheck(_buildingController._taurusHologram)) { //check if the space we are building is valid
                _buildingController.InstantiateMachine(_buildingController._taurusGO);
            }
        }
    }



    public override void OnStateLeave()
    {
        base.OnStateLeave();
        _buildingController._taurusHologram.gameObject.SetActive(false);
    }
}
