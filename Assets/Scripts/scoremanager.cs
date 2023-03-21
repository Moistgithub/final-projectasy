using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class scoremanager : MonoBehaviour
{
    public int currentscore;
    public TMP_Text scoredisplay;
    public void addscore(int scoretoadd)
    {
        currentscore += scoretoadd;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoredisplay == null)
            return;
        scoredisplay.text =currentscore.ToString();
    }
}
