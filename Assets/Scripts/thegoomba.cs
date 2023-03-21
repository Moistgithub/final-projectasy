using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thegoomba : movement
{
    protected Vector2 _moving;
    protected RaycastHit2D _wallchecker;

    protected override void HandleInput()
    {
        checkwall();
    }

    protected virtual void checkwall()
    {
        if(_inputDirection == Vector2.zero) _inputDirection = Vector2.right * Acceleration;

        _wallchecker = Physics2D.Raycast(transform.position, _inputDirection.x > 0 ? Vector2.right : Vector2.left, 1f, GroundLayerMask);

        if (_wallchecker)
            _inputDirection *= -1;
    } 
}
