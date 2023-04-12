using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thegoomba : movement
{
    protected Vector2 _moving;
    protected RaycastHit2D _wallchecker;
    protected RaycastHit2D _enemychecker;

    public Transform left;
    public Transform right;
    public LayerMask EnemyLayermask;
    protected override void HandleInput()
    {
        checkwall();
        checkenemy();
    }

    protected virtual void checkwall()
    {
        if(_inputDirection == Vector2.zero) _inputDirection = Vector2.right * Acceleration;

        _wallchecker = Physics2D.Raycast(transform.position, _inputDirection.x > 0 ? Vector2.right : Vector2.left, 1f, GroundLayerMask);

        if (_wallchecker)
            _inputDirection *= -1;
    } 
    protected virtual void checkenemy()
    {
        Vector2 pos = _inputDirection.x > 0 ? right.position : left.position;
        Vector2 dir = _inputDirection.x > 0 ? Vector2.right : Vector2.left;

        _enemychecker = Physics2D.Raycast(pos, dir, 1f, EnemyLayermask);

        if (_enemychecker)
            _inputDirection *= -1;
    }
}
