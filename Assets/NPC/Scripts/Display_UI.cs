using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class Display_UI : MonoBehaviour
{
    public float geldWert = 0f;
    public float zufriedenheitsWert = 100f;

    public TMP_Text geldAnzeige;
    public TMP_Text zufriedenheitsAnzeige;

    private float maxSatisfaction = 100f;
    private float minSatisfaction = 0f;

    // Update is called once per frame
    void Update()
    {
        if (zufriedenheitsWert > maxSatisfaction)
        {
            zufriedenheitsWert = maxSatisfaction;
        }else if (zufriedenheitsWert < minSatisfaction)
        {
            zufriedenheitsWert = minSatisfaction;
        }
        if (geldWert < 0)
        {
            geldAnzeige.color = new Color32(255, 0, 0, 255);//Red
        }
        if (geldWert > 0)
        {
            geldAnzeige.color = new Color32(0, 255, 0, 255);//green
        }
        geldAnzeige.text = "$"+geldWert;

        zufriedenheitsAnzeige.color = Color.Lerp(new Color(1, 0, 0, 1),new Color(0, 1, 0, 1), zufriedenheitsWert/100);//green->red
        zufriedenheitsAnzeige.text = zufriedenheitsWert+" / 100";
        
    }
}
