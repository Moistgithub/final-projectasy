using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public GameObject DeathScreen;
    private health _health;
    public float Cooldown = 5f;
    // Start is called before the first frame update
    void Start()
    {
        if (!DeathScreen)
            return;
        if (DeathScreen != null)
        {
            DeathScreen.SetActive(false);
        }
        _health = GameObject.Find("player").GetComponent<health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.CurrentHealth <= 0)
        {
            Invoke("ActivateScreen", Cooldown);
        }
    }

    private void ActivateScreen()
    {
        DeathScreen.SetActive(true);
    }
}
