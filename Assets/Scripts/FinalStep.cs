using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalStep : MonoBehaviour
{
    public Animation wrapUp;
    public Animation pourTea;
    public Animation showScore;
    public GameObject button_wrap;
    public GameObject button_finish;
    public GameObject wrap;
    public GameObject tea;
    public Transform nextTrans;
    public Image title;
    public Sprite title2;
    
    public void Wrapup()
    {
        wrapUp.Play();
        button_wrap.SetActive(false);
        StartCoroutine(Wrap());
    }
    IEnumerator Wrap()
    {
        yield return new WaitForSeconds(6.5f);
        button_finish.SetActive(true);
    }

    public void Finish()
    {
        Camera.main.transform.SetPositionAndRotation(nextTrans.position, nextTrans.rotation);
        button_finish.SetActive(false);
        title.sprite = title2;
        wrap.SetActive(false);
        tea.SetActive(true);
        pourTea.Play();
        showScore.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        //Application.Quit();
    }
}
