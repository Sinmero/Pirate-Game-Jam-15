using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform _targetTransform;
    

    void Update()
    {
        transform.position = _targetTransform.position;
    }
}
