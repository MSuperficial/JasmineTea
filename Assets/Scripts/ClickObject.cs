using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ClickObject : MonoBehaviour
{
    public GameObject begin;
    public GameObject choose_m;
    public GameObject choose_e;
    public GameObject frame_empty;
    public GameObject frame_full;
    public GameObject end;
   

    void Start()
    {
       
    }

    public void BeginGame()
    {
        choose_m.SetActive(true);
        choose_e.SetActive(true);
        frame_empty.SetActive(true);
        Destroy(begin);
    }


    public void chooseMorning()
    {
        end.SetActive(true);
        frame_empty.SetActive(false);
        frame_full.SetActive(true);
        Destroy(choose_m);
        Destroy(choose_e);
    }

    public void chooseEvening()
    {
        end.SetActive(true);
        frame_empty.SetActive(false);
        frame_full.SetActive(true);
        Destroy(choose_m);
        Destroy(choose_e);
    }

    public void endGame()
    {
        SceneManager.LoadScene(1);
    }
}