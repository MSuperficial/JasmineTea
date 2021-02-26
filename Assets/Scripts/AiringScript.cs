using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AiringScript: MonoBehaviour
{
    public GameObject Rake_1;
    public GameObject Rake_2;
    public GameObject Temperate;
    public GameObject fangda;
    public GameObject begin;
    public GameObject then;
    public GameObject curing;
    public GameObject Down;
    public GameObject finish;
    public GameObject mix;
    public GameObject NextScene;
    public GameObject FlowerGroup;
    public GameObject ConfirmChoose;
    public GameObject Basket_tall_with_flower;
    public GameObject Basket_tall_with_yulan;
    public GameObject Basket_flat_with_flower;
    public GameObject Basket_flat_with_tea;
    public GameObject flower;
    public Material mat_Flower;
    public Material mat_Transparent;
    public Material mat_Yulan;
    public Material mat_Mix;
    public Slider tempSlider;
    public Slider fangdaSlider;
    public GameObject warningPanel;
    public Text chooseFlowerText;
    public Image flower1;
    public Image flower2;
    public Image flower3;
    public Image flower4;
    public Image title;
    public Sprite title2;
    public Sprite title3;
    private bool isCuring=false;
    //private bool isPause = false;
    //private bool isChange = false;
    private bool flower1_isChoose = false;
    private bool flower2_isChoose = false;
    private bool flower3_isChoose = false;
    private bool flower4_isChoose = false;
    private int count=0;
    private int flowerCount = 0;
    private Animation anim;
    // public string animName = "Animations/RakeAnimation"; //将动画片段的名称用一个共有变量来表示

    void Start()
    {
        anim = Rake_1.GetComponent<Animation>();
        chooseFlowerText.text="";
        tempSlider.value = 30;
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
            Down.SetActive(false);
            count++;
        }
        fangdaSlider.value = tempSlider.value;
    }

    private void changeUI()
    {
        warningPanel.SetActive(true);
    }

    private void Pause()
    {
        tempSlider.value = 30;
        warningPanel.SetActive(false);
    }

    public void BeginGame()
    {
        Destroy(begin);
        StartCoroutine(Begin_Game());
    }

    IEnumerator Begin_Game()
    {
        Basket_tall_with_flower.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1.5f);
        Basket_flat_with_flower.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material = mat_Flower;
        Basket_tall_with_flower.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material = mat_Transparent;
        Temperate.SetActive(true);
        then.SetActive(true);
    }

    public void Fangda()
    {
        fangda.SetActive(!fangda.activeSelf);
    }

    public void Finish()
    {
        FlowerGroup.SetActive(true);
        Rake_1.transform.position = new Vector3(12, -1, 13);
        //isChange = true;
        title.sprite = title2;
        chooseFlowerText.text = "请选择四朵中的其中两朵，作为选择样例，其余花朵将被丢弃";
        ConfirmChoose.SetActive(true);
        Destroy(Temperate);
        Destroy(anim);
        Destroy(finish);
    }

    public void EndChoose()
    {
        if (flowerCount != 2)
        {

        }
        else
        {
            Destroy(FlowerGroup);
            Destroy(ConfirmChoose);
            Basket_flat_with_flower.SetActive(false);
            Basket_flat_with_tea.SetActive(true);
            mix.SetActive(true);
            title.sprite = title3;
            chooseFlowerText.text = "";
            Camera.main.GetComponent<Animation>().Play();
        } 
    }

    public void MixTeaAndYulan()
    {
        StartCoroutine(Mix());
        Destroy(mix);
    }

    public void Next()
    {
        SceneManager.LoadScene(2);
    }

    IEnumerator Mix()
    {
        Basket_tall_with_yulan.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1.5f);
        flower.GetComponent<MeshRenderer>().material = mat_Yulan;
        Basket_tall_with_yulan.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material = mat_Transparent;
        yield return new WaitForSeconds(1.5f);
        Rake_2.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(3f);
        flower.GetComponent<MeshRenderer>().material = mat_Transparent;
        Basket_flat_with_tea.transform.GetChild(1).GetChild(0).GetComponent<MeshRenderer>().material = mat_Mix;
        NextScene.SetActive(true);
    }

    public void Flower1_isChoosed()
    {
        Debug.Log("enter");
        Debug.Log(flower1_isChoose);
        Debug.Log(flowerCount);
        if (!flower1_isChoose)
        {
            if (flowerCount <= 1)
            {
                flower1_isChoose = true;
                flowerCount++;
                flower1.transform.localScale = 1.5f * Vector3.one;
                flower1.GetComponent<Outline>().effectColor = new Color(1f, 1f, 0f, 0.8f);
            }
            else
            {
                chooseFlowerText.text = "请选择四朵中的其中两朵，不能选择过多！！！";
            }
        }
        else
        {
            flower1_isChoose = false;
            flowerCount--;
            flower1.transform.localScale = Vector3.one;
            flower1.GetComponent<Outline>().effectColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }
    }

    public void Flower2_isChoosed()
    {
        if (!flower2_isChoose)
        {
            if (flowerCount <= 1)
            {
                flower2_isChoose = true;
                flowerCount++;
                flower2.transform.localScale = 1.5f * Vector3.one;
                flower2.GetComponent<Outline>().effectColor = new Color(1f, 1f, 0f, 0.8f);
            }
            else
            {
                chooseFlowerText.text = "请选择四朵中的其中两朵，不能选择过多！！！";
            }
        }
        else
        {
            flower2_isChoose = false;
            flowerCount--;
            flower2.transform.localScale = Vector3.one;
            flower2.GetComponent<Outline>().effectColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }
    }

    public void Flower3_isChoosed()
    {
        if (!flower3_isChoose)
        {
            if (flowerCount <= 1)
            {
                flower3_isChoose = true;
                flowerCount++;
                flower3.transform.localScale = 1.5f * Vector3.one;
                flower3.GetComponent<Outline>().effectColor = new Color(1f, 1f, 0f, 0.8f);
            }
            else
            {
                chooseFlowerText.text = "请选择四朵中的其中两朵，不能选择过多！！！";
            }
        }
        else
        {
            flower3_isChoose = false;
            flowerCount--;
            flower3.transform.localScale = Vector3.one;
            flower3.GetComponent<Outline>().effectColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }
    }

    public void Flower4_isChoosed()
    {
        if (!flower4_isChoose)
        {
            if (flowerCount <= 1)
            {
                flower4_isChoose = true;
                flowerCount++;
                flower4.transform.localScale = 1.5f * Vector3.one;
                flower4.GetComponent<Outline>().effectColor = new Color(1f, 1f, 0f, 0.8f);
            }
            else
            {
                chooseFlowerText.text = "请选择四朵中的其中两朵，不能选择过多！！！";
            }
        }
        else
        {
            flower4_isChoose = false;
            flowerCount--;
            flower4.transform.localScale = Vector3.one;
            flower4.GetComponent<Outline>().effectColor = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }
    }

    public void ThenGame()
    {
        curing.SetActive(true);
        Destroy(then);
    }

    public void Curing()
    {
        isCuring = true;
        Down.SetActive(true);
        Destroy(curing);
    }

    public void PlayGame()
    {
        tempSlider.value = 30;
        anim.Play(); // 播放动画
        count++;
    }
}