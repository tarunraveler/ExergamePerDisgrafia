﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {

    private bool active = true;
    private bool inArea;
    public string accettableAreaTag;


    public void KillPoints()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0;
        sr.color = c;
        active = false;
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("triggered " + collision.tag);
        if (collision.tag == accettableAreaTag)
        {
            inArea = true;
            IsochronyButton btn = collision.GetComponent<IsochronyButton>();
            if (btn)
                btn.Clicked();
            else
                collision.GetComponent<Button>().Clicked();
        }
            
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (active && collision.tag == accettableAreaTag)
        {
            inArea = false;
        }
    }

    public void ChangeColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }


}
