using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class weaponhandler : MonoBehaviour
{
    public weapon Currentweapon;

    public Transform UpWeaponPosition;
    public Transform DownWeaponPosition;
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

        if(isFlip == false)
        {
            if(_movement.IsUp)
            {
                Debug.Log("right isup");
                SetWeaponIsUp();
            }
            else if(_movement.IsDown)
            {
                Debug.Log("right isdown");
                SetWeaponIsDown();
            }
            else if(!_movement.IsDown && !_movement.IsUp)
            {
                Debug.Log("isright only");
                SetWeaponRight();
            }
        }
        else if (isFlip == true)
        {
            if (_movement.IsUp)
            {
                Debug.Log("left isup");
                SetWeaponIsUp();
            }
            else if (_movement.IsDown)
            {
                Debug.Log("left isdown");
                SetWeaponIsDown();
            }
            else if (!_movement.IsDown && !_movement.IsUp)
            {
                Debug.Log("isleft only");
                SetWeaponLeft();
            }
        }

    }

    protected virtual void SetWeaponRight()
    {
        Currentweapon.transform.position = RightWeaponPosition.position;
        Currentweapon.transform.rotation = RightWeaponPosition.rotation;
        Currentweapon.IsFlip = false;
        Currentweapon.transform.localScale = new Vector3(1, 1, 1);

    }

    protected virtual void SetWeaponLeft()
    {
        Currentweapon.transform.position = LeftWeaponPosition.position;
        Currentweapon.transform.rotation = LeftWeaponPosition.rotation;
        Currentweapon.IsFlip = true;

        Currentweapon.transform.localScale = new Vector3(-1, 1, 1);

    }

    protected virtual void SetWeaponIsUp( )
    {
        Currentweapon.transform.position = UpWeaponPosition.position;
        Currentweapon.transform.rotation = UpWeaponPosition.rotation;
        Currentweapon.IsFlip = false;

        Currentweapon.transform.localScale = new Vector3(1, 1, 1);

    }


    protected virtual void SetWeaponIsDown( )
    {
        Currentweapon.transform.position = DownWeaponPosition.position;
        Currentweapon.transform.rotation = DownWeaponPosition.rotation;
        Currentweapon.IsFlip = false;


        Currentweapon.transform.localScale = new Vector3(1, 1, 1);

    }
}
