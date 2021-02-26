using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTips : MonoBehaviour
{
    public GameObject content;
    public Text tips_text;
    public List<string> tips = new List<string>();
    private int num = 0;

    public void ShowInfo()
    {
        content.SetActive(true);
        tips_text.text = "点击此处获得提示";
    }

    public void ShowDetail()
    {
        content.SetActive(true);
        tips_text.text = tips[num];
    }

    public void Hide()
    {
        content.SetActive(false);
    }

    public void NextTip()
    {
        num++;
    }
}
