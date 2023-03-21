using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal : MonoBehaviour
{
    public string NextSceneName = "";

    public gamemanager manager;

    private void Start()
    {
        manager = FindObjectOfType<gamemanager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            manager.LoadScene();
        }
    }
}
