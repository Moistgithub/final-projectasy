using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : movement
{
    protected override void HandleInput()
    {
        _inputDirection = new Vector2(Input.GetAxis("Horizontal"), y: 0f);
        if (Input.GetButtonDown("Jump")) 
        {
            if(_canjump)
            {
                FindObjectOfType<AudioManager>().Play("jump", transform.position);
            }
            DoJump();
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            IsUp = true;
            IsDown = false;
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            IsUp = false;
            IsDown = true;
        }
        else
        {
            IsUp = false;
            IsDown = false;
        }
    }
}
