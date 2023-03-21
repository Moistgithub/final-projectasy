using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationhandler : MonoBehaviour
{
    private Animator _animator;
    private movement _movement;
    private Vector3 _initialscale = Vector3.one;
    private Vector3 _flipscale = Vector3.one;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = transform.parent.GetComponent<movement>();  
        
        _initialscale = transform.localScale;   
        _flipscale = new Vector3(-_initialscale.x, _initialscale.y, _initialscale.z);   
    }
    private void HandleFlip()
    {
        if (_movement == null) return;
        if (_movement.FlipAnim)
        {
            transform.localScale = _flipscale;
        }
        else
        {
            transform.localScale = _initialscale;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_animator == null || _movement == null)
            return;
        _animator.SetBool("IsRunning", _movement.IsRunning);

        HandleFlip();
    }
}
