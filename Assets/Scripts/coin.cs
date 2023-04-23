using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class coin : MonoBehaviour
{
    public scoremanager Scoremanager;

    public int coinadd = 10;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (Scoremanager == null)
                return;

            health _health = collider.GetComponent<health>();

            if(_health != null)
            {
                _health.Heal(1f);
            }


            Scoremanager.addscore(coinadd);
            Destroy(gameObject);
        }

    }
    void Start()
    {
        Scoremanager = FindObjectOfType<scoremanager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
