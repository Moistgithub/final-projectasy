using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponhandler : MonoBehaviour
{
    public weapon Currentweapon;

    public Transform RightWeaponPosition;
    public Transform LeftWeaponPosition;    
    protected bool _tryshoot = false;
    protected movement _movement;



    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<movement>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X)) 
            _tryshoot = true;   
        if(Input.GetKeyUp(KeyCode.X))
            _tryshoot=false;

        if (_tryshoot)
            Currentweapon.shoot();
        HandleWeapon();
    }
 
    protected virtual void HandleInput()
    {

    }
    protected virtual void HandleWeapon()
    {
        if (Currentweapon == null)
            return;
        bool isFlip = false;
        if (_movement != null && _movement.FlipAnim) isFlip = true;
        Currentweapon.transform.position = RightWeaponPosition.position;
        if(isFlip)
        {
            Currentweapon.transform.position = LeftWeaponPosition.position;
            Currentweapon.transform.localScale = new Vector3( -1, 1, 1);
        }
        else
        {
            Currentweapon.transform.position = RightWeaponPosition.position;
            Currentweapon.transform.localScale = Vector3.one;
        }

    }
}
