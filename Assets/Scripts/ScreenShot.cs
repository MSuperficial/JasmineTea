using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private static int num = 0;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            num++;
            ScreenCapture.CaptureScreenshot("Screenshot/JasmineScreenShot_" + num + ".png");
            Debug.Log("Captured:JasmineScreenShot_" + num);
        }
    }
}
