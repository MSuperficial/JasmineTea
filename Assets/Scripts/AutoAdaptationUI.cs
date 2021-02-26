using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    width,
    height,
}

[ExecuteAlways]
public class AutoAdaptationUI : MonoBehaviour
{
    public GameObject targetGO;
    public float offset = 0f;
    public Axis axis = Axis.height;
    private Vector3 scale;
    private RectTransform rectTrans;
    private RectTransform targetRectTrans;

    void Start()
    {
        scale = this.GetComponent<RectTransform>().localScale;
        rectTrans = this.GetComponent<RectTransform>();
        targetRectTrans = targetGO.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (axis == Axis.height)
        {
            rectTrans.sizeDelta = new Vector2(rectTrans.sizeDelta.x, (targetRectTrans.sizeDelta.y + offset * 2) / scale.y);
        }
        else if (axis == Axis.width)
        {
            rectTrans.sizeDelta = new Vector2((targetRectTrans.sizeDelta.x + offset * 2) / scale.x, rectTrans.sizeDelta.y);
        }
    }
}
