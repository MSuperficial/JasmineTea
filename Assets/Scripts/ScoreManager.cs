using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ScoreManager : MonoBehaviour
{
    public List<GameObject> gos;

    private static int score = 0;
    private static List<string> comments = new List<string>();
    private static int numofComments = 0;
    private float timer = 0f;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            score = 0;
            comments.Clear();
            numofComments = 0;
            timer = 0f;
        }
    }

    public static int GetScore()
    {
        return score;
    }

    public void AddScore(int add)
    {
        score += add;
        Debug.Log("Add " + add);
        Debug.Log("Score: " + score);
    }

    public void AddComment(string item)
    {
        numofComments++;
        comments.Add(string.Format("{0}.{1}", numofComments, item));
    }

    //摊晒养护
    public void OnTemperatureChange_1(Slider slider)
    {
        if(slider.value > 40)
        {
            timer += Time.deltaTime;
            if(timer >= 0.99f)
            {
                AddScore(-1);
                AddComment("摊晾时的花堆温度过高！");
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    //筛花
    public void ChooseFlowers()
    {
        bool hasFault = false;
        int num = 0;
        for (int i = 0; i < 4; i++)
        {
            if (gos[i].transform.localScale.x != 1)
            {
                num++;
            }
        }
        if (num == 2)
        {
            AddScore(5);
            for (int i = 0; i < 2; i++)
            {
                if (gos[i].transform.localScale.x == 1)
                {
                    AddScore(-1);
                    hasFault = true;
                }
            }
        }
        if (hasFault)
            AddComment("筛花时要筛去未开花朵和青蒂花蕾。");
    }

    //窨花拌和
    public void MixFlowerAndTea()
    {
        int score = 20;
        for (int i = 0; i < 7; i++)
        {
            if (i % 2 == 0 && gos[i].GetComponent<Image>().sprite.name.Equals("moli"))
            {
                score -= 2;
            }
            if (i % 2 != 0 && gos[i].GetComponent<Image>().sprite.name.Equals("tea"))
            {
                score -= 2;
            }
        }
        score = Mathf.Clamp(score, 8, 20);
        AddScore(score);
        if(score < 20)
        {
            AddComment("窨花拌和时要注意茉莉花和茶叶隔层铺开。");
        }
    }

    //静置
    public void Stay(Dropdown time)
    {
        if (time.value == 0)
        {
            AddScore(3);
            AddComment("静置时间以4-5小时为宜。");
        }
        else if (time.value == 1 || time.value == 3)
        {
            AddScore(4);
            AddComment("静置时间以4-5小时为宜。");
        }
        else if (time.value == 2)
        {
            AddScore(5);
        }
    }

    //通花
    public void OnTemperatureChange_2(Slider slider)
    {
        if (slider.value > 40)
        {
            timer += Time.deltaTime;
            if (timer >= 0.99f)
            {
                AddScore(-1);
                AddComment("通花时的花堆温度过高！");
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    //反复窨制
    public void Repeat(Dropdown times)
    {
        int score = times.value * 2 + 4;
        AddScore(score);
        if(score < 10)
        {
            AddComment("重复窨制次数越多越好。");
        }
    }

    //烘焙
    public void DryerHumidity(Text text_humidity)
    {
        float humidity = float.Parse(text_humidity.text);
        if(humidity >=4f && humidity <= 4.5f)
        {
            AddScore(20);
        }
        else
        {
            AddScore(10);
            AddComment("茶叶烘焙后湿度在 4% - 4.5% 为宜。");
        }
    }

    public void ShowScore()
    {
        gos[0].GetComponent<Text>().text = "分数：" + score;
        gos[1].GetComponent<Text>().text += "\n";
        comments.ForEach(delegate (string comment)
        {
            gos[1].GetComponent<Text>().text += comment;
            gos[1].GetComponent<Text>().text += "\n";
        });
    }

    public void UploadScore()
    {
        StartCoroutine(Upload());
    }
    IEnumerator Upload()
    {
        string comment = "";
        comments.ForEach(delegate (string s)
        {
            comment += s;
            comment += "\n";
        });

        WWWForm form = new WWWForm();
        form.AddField("grade", score.ToString());
        form.AddField("comment", comment);

        UnityWebRequest www = UnityWebRequest.Post("http://47.93.237.197:8100/grade/upload-grade", form);
        yield return www.SendWebRequest();
         
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Failed!");
            Debug.Log(www.error);
        }   
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
        }
    }

}
