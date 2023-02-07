using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashController : MonoBehaviour
{
    private Animator anim;
    private Image img;

    private void Start()
    {
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();

        UnLoad();
    }

    public void Load()
    {
        gameObject.SetActive(true);
        anim.SetBool("unload", false);
        anim.SetBool("load", true);
        Invoke("MakeNotTransparent", 1.5f);
    }

    public void UnLoad()
    {
        anim.SetBool("load", false);
        anim.SetBool("unload", true);
        Invoke("MakeTransparent", 1.5f);
    }

    private void MakeTransparent()
    {
        Color transparency = img.color;
        transparency.a = 0;
        gameObject.SetActive(false);
    }

    private void MakeNotTransparent()
    {
        Color transparency = img.color;
        transparency.a = 255;
    }
}
