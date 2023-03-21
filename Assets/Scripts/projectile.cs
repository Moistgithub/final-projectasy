using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float Damage = 1f;

    public float Speed = 10f;
    public float Pushforce = 10f;
    public cooldown Lifetime;
    public LayerMask TargetLayerMask;

    private Rigidbody2D _rigidbody;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Lifetime.StartCooldown();
        _rigidbody.AddRelativeForce(new Vector2(x: Speed, y: 0f));

    }

    // Update is called once per frame
    void Update()
    {
        if (Lifetime.CurrentProgress != cooldown.Progress.Finished)
            return;
        Die();
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!((TargetLayerMask.value & (1 << collision.gameObject.layer)) > 0))
            return;
        Rigidbody2D targetrigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (targetrigidbody != null)
        {
            targetrigidbody.AddForce((collision.transform.position - transform.position).normalized * Pushforce);
        }
        health targethealth = collision.gameObject.GetComponent<health>();
        if (targethealth != null)
        {
            targethealth.Damage(Damage, gameObject);
            Debug.Log("Hit");
        }
        Die();
    }
}
