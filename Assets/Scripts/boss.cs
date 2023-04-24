using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : movement
{
    protected override void HandleInput()
    {
        FindObjectOfType<AudioManager>().Play("boss", transform.position);
        _inputDirection = Vector2.right;
        Debug.Log("sumthing");
    }
}
