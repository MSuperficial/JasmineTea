using System.Net.Mime;
using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DryerSceneAnimation : MonoBehaviour
{
    public Canvas canvas;
    public Image title;
    public Button button_begin;
    public Button button_then;
    public Button button_room;
    public Button button_dry;
    public Button button_repeat;
    public Button button_finish;
    public GameObject routeButton;
    public GameObject image_room;
    public GameObject mix;
    public GameObject flower;
    public GameObject tea;
    public GameObject basket_flower;
    public GameObject basket_tea;
    public GameObject ChooseTimes;
    public Transform secondTransform;
    public Animation door_left;
    public Animation door_right;
    public GameObject dryerLight;
    public GameObject control;
    public Sprite title2;
    public Sprite title3;
    public Text tem;
    public Text hum;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 lastRotation;
    private int lastTemp;
    private bool isRotating = false;
    private int disToAngle = 1;
    private bool isChoosingTem = false;
    private bool isDrying = false;
    private float humidity = 8.5f;
    public Material mattea;

    void Start()
    {
        
    }

    void Update()
    {
        if (isChoosingTem)
        {
            RectTransform rctTr = routeButton.gameObject.GetComponent<RectTransform>();
            //bool isContain = RectTransformUtility.RectangleContainsScreenPoint(rctTr, Input.mousePosition, null);
            if (Input.GetMouseButtonDown(0))
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(rctTr, Input.mousePosition, null))
                {
                    isRotating = true;
                    startPoint = Input.mousePosition;
                    lastRotation = routeButton.transform.localEulerAngles;
                    lastTemp = int.Parse(tem.text);
                }
            }
            if (Input.GetMouseButton(0) && isRotating)
            {
                endPoint = Input.mousePosition;
                float dy = endPoint.y - startPoint.y;
                float angle = dy * disToAngle;
                Debug.Log(lastRotation);
                Debug.Log(angle);
                routeButton.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Clamp(lastRotation.z + angle, 90f, 359f));
                tem.text = Mathf.Clamp(-2 * (int)angle / 3 + lastTemp, 140, 320).ToString();
            }
            if (Input.GetMouseButtonUp(0))
            {
                isRotating = false;
            }
        }
        if (isDrying)
        {
            humidity -= (Time.deltaTime * 0.2f);
            hum.text = humidity.ToString().Substring(0, 4);
        }
    }

    public void Saperate_1()
    {
        mix.SetActive(false);
        flower.SetActive(true);
        tea.SetActive(true);
        button_begin.gameObject.SetActive(false);
        button_then.gameObject.SetActive(true);
    }

    public void Saperate_2()
    {
        flower.SetActive(false);
        tea.SetActive(false);
        basket_flower.SetActive(true);
        basket_tea.SetActive(true);
        button_then.gameObject.SetActive(false);
        button_repeat.gameObject.SetActive(true);
    }

    public void toRepeat()
    {
        title.sprite = title2;
        ChooseTimes.gameObject.SetActive(true);
        button_room.gameObject.SetActive(true);
        button_repeat.gameObject.SetActive(false);
    }

    public void SwitchToRoom()
    {
        title.sprite = title3;
        ChooseTimes.gameObject.SetActive(false);
        button_room.gameObject.SetActive(false);
        StartCoroutine(Switch_Room());
    }

    IEnumerator Switch_Room()
    {
        canvas.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(canvas.GetComponent<Animation>().clip.length);
        Camera.main.transform.SetPositionAndRotation(secondTransform.position, secondTransform.rotation);
        image_room.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Camera.main.GetComponent<Animation>().Play();
        door_left.Play();
        door_right.Play();
        yield return new WaitForSeconds(Camera.main.GetComponent<Animation>().clip.length);
        button_dry.gameObject.SetActive(true);
    }

    public void Dry()
    {
        button_dry.gameObject.SetActive(false);
        control.SetActive(true);
        dryerLight.SetActive(true);
        isChoosingTem = true;
    }

    public void BeginDry()
    {
        isChoosingTem = false;
        isDrying = true;
    }

    public void EndDry()
    {
        isDrying = false;
        button_finish.gameObject.SetActive(true);
    }

    public void Load()
    {
        SceneManager.LoadScene(4);
    }
}
