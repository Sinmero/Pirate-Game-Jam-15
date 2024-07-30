using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCancer : BuildActive
{
    public BuildingCancer(StateMachineHandler stateMachineHandler) : base(stateMachineHandler){}



    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _buildingController._cancerHologram.gameObject.SetActive(true);
    }



    public override void Execute()
    {
        base.Execute();
        _buildingController.FollowHoloBuilding(_buildingController._cancerHologram.gameObject);

        if(Input.GetKeyDown(Controls.keys._confirm)) {
            if(_buildingController.BuildFloorCheck() && _buildingController.BuildOverlapCheck(_buildingController._cancerHologram)) { //check if the space we are building is valid
                _buildingController.InstantiateMachine(_buildingController._cancerGO);
                return;
            }

            AudioManager.instance.PlaySoundClip(AudioManager.instance._buzz);
        }
    }



    public override void OnStateLeave()
    {
        base.OnStateLeave();
        _buildingController._cancerHologram.gameObject.SetActive(false);
    }
}
