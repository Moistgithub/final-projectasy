using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DespawnDelay : MonoBehaviour
{
    private AudioSource audioSource;

    public cooldown cd;

    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource.clip != null)
        {
            cd.Duration = audioSource.clip.length + delay;
        }

        cd.StartCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        if (cd.CurrentProgress == cooldown.Progress.Finished || audioSource.volume == 0) 
            Destroy(gameObject);
    }
}
