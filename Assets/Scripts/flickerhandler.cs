using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickerhandler : MonoBehaviour
{
    public Color color = Color.red;
    public float Duration = 1f;
    public float Interval = 0.2f;

    private Color _originalcolor;
    private health _healthy;
    private SpriteRenderer _spriteRenderer;
    private bool _canstartflicker = false;
    // Start is called before the first frame update
    void Start()
    {
        _healthy = GetComponentInParent<health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_spriteRenderer != null )    
            _originalcolor = _spriteRenderer.color;

        if(_healthy != null)
        {
            _healthy.onhit += Flicker;
            _healthy.onhitreset += StopFlicker;
        }
    }
    private void OnDisable()
    {
        if (_healthy != null)
        {
            _healthy.onhit -= Flicker;
            _healthy.onhitreset -= StopFlicker;
        }
    }
    private void Flicker(GameObject source)
    {
        _canstartflicker = true;
        StartCoroutine(DoFlicker());
    }
    private void StopFlicker()
    {
        _canstartflicker = false;
    }
    IEnumerator DoFlicker()
    {
        float duration = Duration;
        float flickerduration = Interval;

        Color _flickerColor = color;
        bool Colorflicker = false;

        while (duration > 0)
        {
            if (flickerduration <= 0)
            {
                Colorflicker = !Colorflicker;
                flickerduration = Interval;
            }
            if (Colorflicker)
            {
                _spriteRenderer.color = _flickerColor;
            }
            else
            {
                _spriteRenderer.color = _originalcolor;

            }
            flickerduration -= Time.deltaTime;
            duration -= Time.deltaTime;
            yield return null;
        }
        _spriteRenderer.color = _originalcolor;
    }
}
