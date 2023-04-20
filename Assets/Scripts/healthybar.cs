using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthybar : MonoBehaviour
{
    public health hp;
    public Image image;
    void Update()
    {
        Debug.Log(hp.CurrentHealth / hp.maxhealth);
        image.fillAmount = hp.CurrentHealth / hp.maxhealth;
    }
}

