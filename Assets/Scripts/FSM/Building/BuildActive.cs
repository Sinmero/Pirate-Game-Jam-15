using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildActive : State
{
    public BuildingController _buildingController;

    public BuildActive(StateMachineHandler stateMachineHandler) : base(stateMachineHandler){
        _buildingController = _stateMachineHandler as BuildingController;
    }



    public override void Execute()
    {
        base.Execute();

        if(Input.GetKeyDown(Controls.keys._key1)) {
            _buildingController.ChangeState(_buildingController._buildingMiner);
            return;
        }
        if(Input.GetKeyDown(Controls.keys._key2)) {
            _buildingController.ChangeState(_buildingController._buildingFactory);
            return;
        }
        if(Input.GetKeyDown(Controls.keys._key3)) {
            _buildingController.ChangeState(_buildingController._buildingTaurus);
            return;
        }
        if(Input.GetKeyDown(Controls.keys._key4)) {
            _buildingController.ChangeState(_buildingController._buildingCancer);
            return;
        }
        if(Input.GetKeyDown(Controls.keys._key5)) {
            _buildingController.ChangeState(_buildingController._buildingSolidifyer);
            return;
        }
        if(Input.GetKeyDown(Controls.keys._buildMenu)) {
            _buildingController.ChangeState(_buildingController._buildIdle);
        }
    }
}
