using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    //old cam ortho 5.034289
    public Transform _targetTransform;
    

    void Update()
    {
        transform.position = _targetTransform.position;
    }
}
