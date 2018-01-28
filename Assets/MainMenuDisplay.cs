using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuDisplay : MonoBehaviour
{

    public Sprite credits;
    public Sprite instructions;

    private Image img;
    // Use this for initialization
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCredits()
    {
        img.enabled = true;
        img.sprite = credits;
    }

    public void SetInstructions()
    {
        img.enabled = true;
        img.sprite = instructions;
    }

    public void Clear()
    {
        img.enabled = false;
    }
}
