using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goombahealth : health
{
    public float offsetY = 0.4f;

    public override void Damage(float damageAmount, GameObject source)
    {
        if (!_candamage)
            return;

        if (source.transform.position.y < transform.position.y + offsetY) return;

        _currenthealth -= damageAmount;
        if (_currenthealth <= 0)
        {
            Die();
        }
        invulnerable.StartCooldown();
        _candamage = false;
        onhit?.Invoke(source);
    }
}
