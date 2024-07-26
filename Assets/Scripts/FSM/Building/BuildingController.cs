using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingController : StateMachineHandler
{
    [HideInInspector] public BuildActive _buildActive;
    [HideInInspector] public BuildIdle _buildIdle;
    [HideInInspector] public BuildingFactory _buildingFactory;
    [HideInInspector] public BuildingMiner _buildingMiner;
    private float _machineSize = 4;
    private LayerMask _solid;
    private LayerMask _machines;


    public GameObject _buildingUI;
    public MachineHologram _factoryHologram;
    public MinerHologram _minerHologram;
    public GameObject _factoryGO;
    public GameObject _minerGO;



    private void Start()
    {
        _buildActive = new BuildActive(this);
        _buildIdle = new BuildIdle(this, _buildingUI);
        _buildingFactory = new BuildingFactory(this);
        _buildingMiner = new BuildingMiner(this);

        _solid = LayerMask.GetMask("Solid");
        _machines = LayerMask.GetMask("Machines");

        ChangeState(_buildIdle);
    }



    private Vector3 _newPosition = new Vector3();
    public void FollowHoloBuilding(GameObject targetGO)
    { //make the building hologram snaps to grid
        _newPosition.x = Mathf.Floor(transform.position.x + 0.5f);
        _newPosition.y = MathF.Floor(transform.position.y) + 2f;
        targetGO.transform.position = _newPosition;
    }



    public bool BuildFloorCheck()
    { //check if there are any empty tiles below the building
        Vector3 offset = new Vector3(0, -2 + 0.1f, 0);

        for (int i = 0; i < _machineSize; i++)
        {

            offset.x = i - _machineSize * 0.5f + 0.5f;

            RaycastHit2D[] collisions = Physics2D.RaycastAll(_newPosition + offset, Vector2.down, 0.5f, _solid);

            Debug.DrawRay(_newPosition + offset, new Vector2(0, -1), Color.red);

            if (collisions.Length == 0) {
                PhysicsLogger.instance.Log($"Empty tile detected below the building", this);
                return false;
            }
        }

        return true;
    }



    public bool BuildOverlapCheck()
    { //check if there any overlaps with solid ground or other machines
        if(OverlapCheck(_solid) && _factoryHologram._machineList.Count == 0) {
            Debug.Log(_factoryHologram._machineList.Count + " " + OverlapCheck(_solid));
            PhysicsLogger.instance.Log($"Nothing is overlapping", this);
            return true;
        }
        return false;
    }


    public bool OverlapCheck(LayerMask layerMask) {
        RaycastHit2D[] overlapHit = Physics2D.BoxCastAll(_newPosition, new Vector2(_machineSize, _machineSize) * 0.9f, 0, Vector2.zero, 0, _solid);
        if (overlapHit.Length > 0)
        {
            PhysicsLogger.instance.Log($"Building is overlapping with {overlapHit[0].collider.name}", this);
            return false;
        }

        return true;
    }



    public GameObject InstantiateMachine (GameObject machine) {
        var newMachine = Instantiate(machine, _newPosition, quaternion.identity, transform.parent);
        // newMachine.layer = _machines;
        SystemLogger.instance.Log($"{machine} was instantiated at {_newPosition}", this);
        return newMachine;
    }



    public GameObject InstantiateMachine (GameObject machine, Vector3 position) {
        var newMachine = Instantiate(machine, position, quaternion.identity, transform.parent);
        // newMachine.layer = _machines;
        SystemLogger.instance.Log($"{machine} was instantiated at {_newPosition}", this);
        return newMachine;
    }
}
