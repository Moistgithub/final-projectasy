using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : movement
{
    protected override void HandleInput()
    {
        _inputDirection = Vector2.right;
        Debug.Log("sumthing");
    }
}
