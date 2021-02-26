using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PourAnimation : MonoBehaviour
{
    public bool startToDetect = false;
    public GameObject flower_tea;
    public GameObject button_mix;
    public Material flower;
    public Material tea;
    public Image[] image = new Image[7];
    public Sprite cha;
    public Sprite moli;
    private Ray ray;
    private RaycastHit hitInfo;
    private Animation anim;
    private int i = 6;
    void Start()
    {
        
    }

    void Update()
    {
        if(startToDetect && Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hitInfo))
            {
                if(hitInfo.collider.gameObject.name.Contains("Basket"))
                {
                    anim = hitInfo.collider.GetComponent<Animation>();
                    anim.Play();
                    startToDetect = false;
                    StartCoroutine(ChangeMaterial());
                }
            }
        }
        if(anim != null && anim.isPlaying == false && i >= 0)
        {
            startToDetect = true;
            anim = null;
        }
    }

    IEnumerator ChangeMaterial()
    {
        yield return new WaitForSeconds(1.5f);
        MeshRenderer renderer = flower_tea.GetComponent<MeshRenderer>();
        if(hitInfo.collider.gameObject.name.Contains("flower"))
        {
            renderer.material = flower;
            image[i].sprite = moli;
            i--;
            if (i == -1)
            {
                button_mix.SetActive(true);
                startToDetect = false;
            }
        }
        else
        {
            renderer.material = tea;
            image[i].sprite = cha;
            i--;
            if (i == -1)
            {
                button_mix.SetActive(true);
                startToDetect = false;
            }
        }
        yield return null;
    }
}
