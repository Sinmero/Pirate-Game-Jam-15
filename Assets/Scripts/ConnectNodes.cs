using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class ConnectNodes : MonoBehaviour
{
    private static ConnectNodes _conNodes;
    public static ConnectNodes instance { get { return _conNodes; } set { return; } }
    [HideInInspector] public ConnectionNode _currentConnectionNode;
    [HideInInspector] public NodeState _prevNodeState;
    public delegate void OnUpdate();
    public event OnUpdate onUpdate;
    [HideInInspector] public LineRenderer _currentLineRenderer;
    [HideInInspector] public Vector3 _startPos;

    void Start()
    {
        if (_conNodes != null) Destroy(this);
        _conNodes = this;
    }

    void Update()
    {
        onUpdate?.Invoke();
    }


    public void Connect()
    {
        ConnectLine(_startPos, Globals.instance._playerGO.transform.position, _currentLineRenderer); //end position here should always be player's position
    }



    public void ConnectLine(Vector3 startPos, Vector3 endPos, LineRenderer lineRenderer)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }



    public void Clear() {
        onUpdate = null;
        _currentConnectionNode = null;
        _currentLineRenderer = null;
        _prevNodeState = null;
    }



    public void ClearLine() {
        SystemLogger.instance.Log($"Destroying {_currentLineRenderer.name}", this);
        Destroy(_currentLineRenderer.gameObject);

    }



    public void ClearLine(LineRenderer lineRenderer) {
        SystemLogger.instance.Log($"Destroying {lineRenderer.name}", this);
        Destroy(lineRenderer.gameObject);
    }
}
