using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildIdle : State
{
    private GameObject _buildToolUI;
    public BuildingController _buildingController;


    public BuildIdle(StateMachineHandler stateMachineHandler, GameObject BuildToolUI) : base(stateMachineHandler){
        _buildToolUI = BuildToolUI;
        _buildingController = _stateMachineHandler as BuildingController;
    }


    public override void OnStateEnter()
    {
        base.OnStateEnter();
        _buildToolUI.SetActive(false);
    }



    public override void Execute()
    {
        base.Execute();
        if(Input.GetKeyDown(Controls.keys._buildMenu)){
            _buildToolUI.SetActive(true);
            _buildingController.ChangeState(_buildingController._buildActive);
        }
    }


    public override void OnStateLeave()
    {
        base.OnStateLeave();
    }

}
