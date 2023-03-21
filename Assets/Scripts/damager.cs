using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class damager : MonoBehaviour
{
    public float Damage = 1f;
    public LayerMask TargetLayerMask;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!((TargetLayerMask.value & (1 << collision.gameObject.layer)) > 0))
        return;
        health targethealth = collision.gameObject.GetComponent<health>();

        if (targethealth == null)
            return;
        targethealth.Damage(Damage, transform.gameObject);
    }
}
