using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSolidifyer : BuildActive
{
    public BuildingSolidifyer(StateMachineHandler stateMachineHandler) : base(stateMachineHandler){}



    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _buildingController._solidifyerHologram.gameObject.SetActive(true);
    }



    public override void Execute()
    {
        base.Execute();
        _buildingController.FollowHoloBuilding(_buildingController._solidifyerHologram.gameObject);

        if(Input.GetKeyDown(Controls.keys._confirm)) {
            if(_buildingController.BuildFloorCheck() && _buildingController.BuildOverlapCheck(_buildingController._solidifyerHologram)) { //check if the space we are building is valid
                _buildingController.InstantiateMachine(_buildingController._solidifyerGO);
            }
        }
    }



    public override void OnStateLeave()
    {
        base.OnStateLeave();
        _buildingController._solidifyerHologram.gameObject.SetActive(false);
    }
}
