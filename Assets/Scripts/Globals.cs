using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    private static Globals _glbs;
    public static Globals instance {get{return _glbs;} set{return;}}
    public GameObject _playerGO;



    private void Start() {
        if(_glbs != null) {
            Destroy(this);
        }
        _glbs = this;
    }
}
