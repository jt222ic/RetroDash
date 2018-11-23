using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour {

    Quaternion iniRot = Quaternion.identity;
 
 void Start()
    {
        iniRot = transform.rotation;
    }

    void LateUpdate()
    {
        transform.rotation = iniRot;
    }
}
