using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : movement
{
    protected override void HandleInput()
    {
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), y: 0f);
        if(Input.GetButtonDown("Jump"))
            DoJump();
    }
}
