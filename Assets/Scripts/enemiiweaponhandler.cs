using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemiiweaponhandler : MonoBehaviour
{
    public weapon Currentweapon;

    protected bool _tryshoot = false;
    protected movement _movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Currentweapon != null)
        {
            _tryshoot = true;
            Currentweapon.shoot();
        }
    }
}
