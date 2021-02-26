using System;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NightSceneController : MonoBehaviour
{
    public GameObject button_Begin;
    public GameObject infoPanel;
    public Text text_Caption;
    public GameObject basketWithFlower;
    public GameObject basketWithTea;
    public GameObject mainCamera;
    public GameObject ImageGroup;
    public GameObject mix;
    public GameObject next;
    public Image[] image = new Image[7];
    public Sprite moli_cha;
    public Sprite title2;
    public Sprite title3;
    public GameObject Rake;
    public GameObject ChooseTime;
    public GameObject downTemp;
    public GameObject clickDowmTemp;
    public GameObject Temperate;
    public GameObject fangda;
    public Image title;
    public GameObject flowerOrTea;
    public GameObject flowerAndTea;
    public Slider tempSlider;
    public Slider fangdaSlider;
    public GameObject finish;
    private bool isCuring = false;
    private int count = 0;
    private Animation anim;
    void Start()
    {

    }

    void Update()
    {
        if (count <= 4)
        {
            if (isCuring)
            {
                tempSlider.value += (Time.deltaTime * 2);
                if (tempSlider.value > 40)
                {
                    changeUI();
                    Invoke("Pause", 2);
                }
            }
        }
        if (count == 5)
        {
            tempSlider.value = 35;
            finish.SetActive(true);
            Destroy(clickDowmTemp);
            count++;
        }
        fangdaSlider.value = tempSlider.value;
    }

    private void changeUI()
    {
        infoPanel.SetActive(true);
        text_Caption.text = "警告!!!请注意温度，及时降温.否则会影响品质";
    }

    private void Pause()
    {
        infoPanel.SetActive(true);
        tempSlider.value = 30;
        text_Caption.text = "请使用耙子给花堆降温，与晾晒相似";
    }

    public void ClickDownTemp()
    {
        tempSlider.value = 30;
        anim.Play(); // 播放动画
        count++;
    }

    public void Begin()
    {
        infoPanel.SetActive(true);
        button_Begin.SetActive(false);
        text_Caption.text = "点击相应箩筐倾倒茉莉花和茶叶";
        mainCamera.GetComponent<Animation>().Play();
        basketWithFlower.GetComponent<BoxCollider>().enabled = true;
        basketWithTea.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<PourAnimation>().startToDetect = true;
        ImageGroup.SetActive(true);
    }

    public void PlayMix()
    {
        infoPanel.SetActive(false);
        anim = Rake.GetComponent<Animation>();
        anim.Play();
        Destroy(mix);
        StartCoroutine(Mix());
    }
    IEnumerator Mix()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 7; i++)
        {
            image[i].sprite = moli_cha;
        }
        flowerOrTea.SetActive(false);
        flowerAndTea.SetActive(true);
        next.SetActive(true);
    }

    public void toMotionless()
    {
        Destroy(ImageGroup);
        Destroy(next);
        title.sprite = title2;
        ChooseTime.SetActive(true);
        downTemp.SetActive(true);
    }

    public void toDownTemp()
    {
        infoPanel.SetActive(true);
        title.sprite = title3;
        text_Caption.text = "请使用耙子给花堆降温，与晾晒相似";
        Temperate.SetActive(true);
        clickDowmTemp.SetActive(true);
        Destroy(downTemp);
        Destroy(ChooseTime);
        isCuring = true;
    }

    public void Fangda()
    {
        fangda.SetActive(!fangda.activeSelf);
    }

    public void Finish()
    {
        SceneManager.LoadScene(3);
    }
}
