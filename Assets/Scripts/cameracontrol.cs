using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 PositionOffset = Vector2.zero;
    public float LerpSpeed = 5f;

    protected float targetXPos = 0f;
    private GameObject _player;
    protected float targetYPos = 0f;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    protected float targetposition;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_player == null)
            return;
        targetXPos = Mathf.Lerp(targetXPos, _player.transform.position.x, Time.deltaTime *LerpSpeed);
        targetYPos = Mathf.Lerp(targetYPos, _player.transform.position.y, Time.deltaTime *LerpSpeed);
        //transform.position = new Vector3(targetXPos, targetYPos, -10f);
        float clampedX = Mathf.Clamp(targetXPos, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(targetYPos, minPosition.y, maxPosition.y);
        transform.position = new Vector3(clampedX, clampedY, -10f);

    }
}
