using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2 : movement
{
    protected override void HandleInput()
    {
        _inputDirection = Vector3.up;
        Debug.Log("sumthing");
    }
}
