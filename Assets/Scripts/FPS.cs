using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public float updateInterval = 1.0f;
    public Text text_FPS;
    private float passedTime = 0f;
    private int passedFrame = 0;
    private float realTimeFPS = 0f;

    void Update()
    {
        GetRealTimeFPS();
        ShowFPS();
    }

    void GetRealTimeFPS()
    {
        passedFrame++;
        passedTime += Time.deltaTime;
        if(passedTime >= updateInterval)
        {
            realTimeFPS = passedFrame / passedTime;
            passedFrame = 0;
            passedTime = 0;
        }
    }

    void ShowFPS()
    {
        text_FPS.text = "  FPS: " + (int)realTimeFPS;
    }
}
