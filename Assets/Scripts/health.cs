using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class health : MonoBehaviour
{
    //public
    public delegate void HitEvent(GameObject source);
    public HitEvent onhit;
    public delegate void ResetEvent();
    public ResetEvent onhitreset;
    public float CurrentHealth
    {
        get
        {
            return _currenthealth;
        }
    }
    public GameObject DeathParticles;

    //private
    public cooldown invulnerable;
    public float _currenthealth = 10f;
    public bool _candamage = true;
    public virtual void Damage(float damageAmount, GameObject source)
    {
        if (!_candamage)
            return;
        _currenthealth -= damageAmount;
        if (_currenthealth <= 0)
        {
            Die();
        }
        invulnerable.StartCooldown();
        _candamage = false;
        onhit?.Invoke(source);
    }

    private void Update()
    {
        ResetInvulnerable();
    }

    private void ResetInvulnerable()
    {
        if (_candamage)
            return;
        if (invulnerable.IsOnCooldown && _candamage == false)
            return;
        _candamage = true;
        onhitreset?.Invoke();
    }
    public void Die()
    {
        GameObject.Instantiate(DeathParticles, transform.position, transform.rotation);
        Debug.Log("died");
        Destroy(this.gameObject);
    }
}
