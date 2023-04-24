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
    public float maxhealth;
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
        if (gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("cathurt", transform.position);
        }
        onhit?.Invoke(source);
    }

    public void Start()
    {
        float maxhealth = _currenthealth;

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

    public void Heal(float amount)
    {
        float projection = _currenthealth + amount;

        switch (projection)
        {
            case float i when i >= maxhealth:
                _currenthealth = maxhealth;
                break;

            case float i when i < maxhealth:
                _currenthealth = projection;
                break;
        }
    }

    public void Die()
    {
        switch (gameObject.tag)
        {
            case "Enemy":
                FindObjectOfType<AudioManager>().Play("enemydeath", transform.position);
                break;


            case "Boss":
                FindObjectOfType<AudioManager>().Play("bossdeath", transform.position);
                break;
        }

        GameObject.Instantiate(DeathParticles, transform.position, transform.rotation);
        Debug.Log("died");

        Destroy(this.gameObject);
    }

}
