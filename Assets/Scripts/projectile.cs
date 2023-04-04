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
    public bool IsFlip = false;
    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody;

    public float dir = 1f;
    public float updowndir = 1f;

    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        _rigidbody = GetComponent<Rigidbody2D>();
        Lifetime.StartCooldown();
        _rigidbody.AddRelativeForce(new Vector2(x: Speed * dir, y: 0f));

        if (IsFlip)
        {
            _spriteRenderer.transform.localScale = new Vector3(_spriteRenderer.transform.localScale.x * -1, _spriteRenderer.transform.localScale.y, _spriteRenderer.transform.localScale.z);
        }

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
