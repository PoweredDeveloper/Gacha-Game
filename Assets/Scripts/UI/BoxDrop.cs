using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxDrop : MonoBehaviour
{
    public Animator anim;
    public SplashController sc;

    public Color unique;
    public Color legendary;
    public Color epic;
    public Color rare;
    public Color nothing;

    public void Drop(string highestRarity)
    {
        transform.GetChild(0).GetComponent<Image>().color = nothing;
        switch (highestRarity) {
            case "unique":
                {
                    transform.GetChild(0).GetComponent<Image>().color = unique;
                    break;
                }
            case "legendary":
                {
                    transform.GetChild(0).GetComponent<Image>().color = legendary;
                    break;
                }
            case "epic":
                {
                    transform.GetChild(0).GetComponent<Image>().color = epic;
                    break;
                }
            case "rare":
                {
                    transform.GetChild(0).GetComponent<Image>().color = rare;
                    break;
                }
            case "null":
                {
                    transform.GetChild(0).GetComponent<Image>().color = nothing;
                    break;
                }
        }

        gameObject.SetActive(true);
        anim.SetBool("shake", false);
        anim.SetBool("fadeOut", false);
        anim.SetBool("drop", true);
        Invoke("setPosition", 0.3f);
    }

    public void Shake()
    {
        anim.SetBool("fadeOut", false);
        anim.SetBool("drop", false);
        anim.SetBool("shake", true);

        Invoke("fadeOut", 2.5f);
    }

    public void fadeOut()
    {
        anim.SetBool("drop", false);
        anim.SetBool("shake", false);
        anim.SetBool("fadeOut", true);

        Invoke("Desapear", 1f);
    }

    private void setPosition()
    {
        Invoke("Shake", 0);
    }

    private void Desapear()
    {
        gameObject.SetActive(false);
        sc.UnLoad();
    }
}
