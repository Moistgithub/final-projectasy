using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject Projectile;
    public Transform SpawnPos;
    public cooldown ShootInterval;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ShootInterval.CurrentProgress);

        if (ShootInterval.CurrentProgress != cooldown.Progress.Finished)
            return;

        ShootInterval.CurrentProgress = cooldown.Progress.Ready;
    }
    public void shoot()
    {
        if (ShootInterval.CurrentProgress != cooldown.Progress.Ready)
            return;

        GameObject bullet = Instantiate(Projectile,SpawnPos.position, SpawnPos.rotation);
        ShootInterval.StartCooldown();

    }    
}
